using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Utils;
using MasterLibrary.Views.MessageBoxML;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace MasterLibrary.ViewModel.CustomerVM.BookCartVM
{
    public class BookCartViewModel: BaseViewModel
    {
        #region thuộc tính
        private ObservableCollection<BookInCartDTO> _ListBooksInCart;
        public ObservableCollection<BookInCartDTO> ListBooksInCart
        {
            get { return _ListBooksInCart; }
            set { _ListBooksInCart = value; OnPropertyChanged(); }
        }

        private BookInCartDTO _SelectedBookInCart;
        public BookInCartDTO SelectedBookInCart
        {
            get { return _SelectedBookInCart; }
            set { _SelectedBookInCart = value; OnPropertyChanged(); }
        }

        private decimal _TongTien;
        public decimal TongTien
        {
            get { return _TongTien; }
            set { _TongTien = value; OnPropertyChanged(); }
        }

        private string _TongTienStr;
        public string TongTienStr
        {
            get { return _TongTienStr; }
            set { _TongTienStr = value; OnPropertyChanged(); }
        }

        private int _TongSach;
        public int TongSach
        {
            get { return _TongSach; }
            set { _TongSach = value; OnPropertyChanged(); }
        }

        #endregion

        #region Icommand
        public ICommand FirstLoadBookInCart { get; set; }
        public ICommand PlusCommand { get; set; }
        public ICommand MinusCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DeleteAllCommand { get; set; }
        public ICommand PayAllCommand { get; set; }
        public ICommand ChecktxbQuantity { get; set; }
        public ICommand IsAllowedInput { get; set; }
        
        #endregion

        public BookCartViewModel()
        {
            FirstLoadBookInCart = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                ListBooksInCart = new ObservableCollection<BookInCartDTO>(await BookInCartServices.Ins.GetAllBookInCart(MainCustomerViewModel.CurrentCustomer.MAKH));
                ReCalculateMoney();
                ReCalculateQuantity();
            });

            PlusCommand = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                BookInCartDTO BookInCartCurrent = SelectedBookInCart;

                if (BookInCartCurrent != null)
                {
                    for (int i = 0; i < ListBooksInCart.Count; i++)
                    {
                        if (BookInCartCurrent.MaSach == ListBooksInCart[i].MaSach)
                        {
                            if (BookInCartCurrent.SoLuongHT + 1 <= ListBooksInCart[i].SoLuongMax)
                            {
                                ListBooksInCart[i].SoLuongHT += 1;
                                FilterBookInCart();
                                ReCalculateMoney();
                                ReCalculateQuantity();

                                await BookInCartServices.Ins.AddBookInCart(MainCustomerViewModel.CurrentCustomer.MAKH, BookInCartCurrent.MaSach, 1, BookInCartCurrent.SoLuongMax);
                            }
                            else
                            {
                                MessageBoxML ms = new MessageBoxML("Thông báo", "Số lượng đã đạt đến tối đa", MessageType.Error, MessageButtons.OK);
                                ms.ShowDialog();
                            }

                            break;
                        }
                    }
                }
            });

            MinusCommand = new RelayCommand<object>((p) => { return true; },  async (p) =>
            {
                BookInCartDTO BookInCartCurrent = SelectedBookInCart;

                if (BookInCartCurrent != null)
                {
                    for (int i = 0; i < ListBooksInCart.Count; i++)
                    {
                        if (BookInCartCurrent.MaSach == ListBooksInCart[i].MaSach)
                        {
                            if (BookInCartCurrent.SoLuongHT >= 1)
                            {
                                if (BookInCartCurrent.SoLuongHT == 1)
                                {
                                    await deleteBook(BookInCartCurrent);
                                }
                                else
                                {
                                    ListBooksInCart[i].SoLuongHT -= 1;
                                    await BookInCartServices.Ins.ReduceBookInCart(MainCustomerViewModel.CurrentCustomer.MAKH, BookInCartCurrent.MaSach, 1);

                                }

                                FilterBookInCart();
                                ReCalculateMoney();
                                ReCalculateQuantity();
                            }

                            break;
                        }
                    }
                }
            });

            DeleteCommand = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                BookInCartDTO BookInCartCurrent = SelectedBookInCart;

                if (BookInCartCurrent != null)
                {
                    await deleteBook(BookInCartCurrent);
                }
            });

            DeleteAllCommand = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (TongSach == 0)
                {
                    MessageBoxML mx = new MessageBoxML("Thông báo", "Giỏ hàng không có gì để xoá", MessageType.Error, MessageButtons.OK);
                    mx.ShowDialog();

                    return;
                }

                MessageBoxML ms = new MessageBoxML("Xác nhận", "Xoá tất cả vật phẩm trong giỏ", MessageType.Waitting, MessageButtons.YesNo);

                if (ms.ShowDialog() == true)
                {
                    (bool isDeleteAll, string lb) = await BookInCartServices.Ins.DeleteAllBookInCart(MainCustomerViewModel.CurrentCustomer.MAKH);

                    if (isDeleteAll == true)
                    {
                        MessageBoxML mb = new MessageBoxML("Thông báo", lb, MessageType.Accept, MessageButtons.OK);
                        mb.ShowDialog();

                        ListBooksInCart.Clear();

                        FilterBookInCart();
                        ReCalculateMoney();
                        ReCalculateQuantity();
                    }
                    else
                    {
                        MessageBoxML mb = new MessageBoxML("Lỗi", lb, MessageType.Accept, MessageButtons.OK);
                        mb.ShowDialog();
                    }
                }
            });

            PayAllCommand = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (TongSach == 0)
                {
                    MessageBoxML mx = new MessageBoxML("Thông báo", "Số lượng sách bằng 0 nên không thể thanh toán", MessageType.Error, MessageButtons.OK);
                    mx.ShowDialog();

                    return;
                }

                MessageBoxML ms = new MessageBoxML("Xác nhận", "Bạn muốn thanh toán", MessageType.Waitting, MessageButtons.YesNo);

                if (ms.ShowDialog() == true)
                {
                    decimal totalTien = TongTien;

                    List<BillDetailDTO> newbillDetailList = new List<BillDetailDTO>();

                    for (int i = 0; i < ListBooksInCart.Count; ++i)
                    {
                        if (ListBooksInCart[i].SoLuongHT > ListBooksInCart[i].SoLuongMax)
                        {
                            MessageBoxML mb = new MessageBoxML("Lỗi", "Số lượng không phù hợp với vài vật phẩm", MessageType.Accept, MessageButtons.OK);
                            mb.ShowDialog();

                            return;
                        }

                        newbillDetailList.Add(
                            new BillDetailDTO {
                                MaSach = ListBooksInCart[i].MaSach,
                                SoLuong = ListBooksInCart[i].SoLuongHT,
                                GiaMoiCai = ListBooksInCart[i].Gia
                            });
                    }

                    BillDTO bill = new BillDTO
                    {
                        NGHD = DateTime.Now,
                        MAKH = MainCustomerViewModel.CurrentCustomer.MAKH,
                        TRIGIA = totalTien,
                    };

                    // Tạo hoá đơn mới
                    int billId = await BuyServices.Ins.CreateNewBill(bill);

                    //Tạo các chi tiết hoá đơn

                    (bool isCreate, string lb) = await BuyServices.Ins.CreateNewBillDetail(billId, newbillDetailList);

                    if (isCreate == true)
                    {
                        MessageBoxML mb = new MessageBoxML("Thông báo", "Thanh toán thành công", MessageType.Accept, MessageButtons.OK);
                        mb.ShowDialog();

                        ListBooksInCart.Clear();

                        FilterBookInCart();
                        ReCalculateMoney();
                        ReCalculateQuantity();

                        await BookInCartServices.Ins.DeleteAllBookInCart(MainCustomerViewModel.CurrentCustomer.MAKH);

                    }
                    else
                    {
                        MessageBoxML mb = new MessageBoxML("Thông báo", lb, MessageType.Error, MessageButtons.OK);
                        mb.ShowDialog();
                    }
                }
            });
        }

        public void FilterBookInCart()
        {
            ListBooksInCart = new ObservableCollection<BookInCartDTO>(ListBooksInCart);
        }

        public void ReCalculateMoney()
        {
            decimal totalMoney = 0;
            for (int i = 0; i < ListBooksInCart.Count; ++i)
            {
                totalMoney += ListBooksInCart[i].Gia * ListBooksInCart[i].SoLuongHT; 
            }

            TongTien = totalMoney;
            TongTienStr = Helper.FormatVNMoney(totalMoney);
        }

        public void ReCalculateQuantity()
        {
            int totalQuantity = 0;

            for (int i = 0; i < ListBooksInCart.Count; ++i)
            {
                totalQuantity += ListBooksInCart[i].SoLuongHT;
            }

            TongSach = totalQuantity;
        }

        public async Task deleteBook(BookInCartDTO BookInCartCurrent)
        {
            MessageBoxML ms = new MessageBoxML("Xác nhận", "Xoá vật phẩm này", MessageType.Waitting, MessageButtons.YesNo);

            if (ms.ShowDialog() == true)
            {
                int indexDelete = -1;

                for (int i = 0; i < ListBooksInCart.Count; i++)
                {
                    if (BookInCartCurrent.MaSach == ListBooksInCart[i].MaSach)
                    {
                        (bool isDelete, string lb) = await BookInCartServices.Ins.DeleteBookInCart(MainCustomerViewModel.CurrentCustomer.MAKH, BookInCartCurrent.MaSach);

                        if (isDelete == true)
                        {
                            MessageBoxML mb = new MessageBoxML("Thông báo", lb, MessageType.Accept, MessageButtons.OK);
                            indexDelete = i;
                            mb.ShowDialog();
                        }
                        else
                        {
                            MessageBoxML mb = new MessageBoxML("Lỗi", lb, MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                        }

                        break;
                    }
                }

                if (indexDelete != -1)
                {
                    ListBooksInCart.RemoveAt(indexDelete);

                    FilterBookInCart();
                    ReCalculateMoney();
                    ReCalculateQuantity();
                }
            }
        }
    }
}
