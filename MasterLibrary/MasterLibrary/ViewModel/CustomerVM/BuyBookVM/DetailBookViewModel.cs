using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Utils;
using MasterLibrary.Views.Customer.BuyBookPage;
using MasterLibrary.Views.MessageBoxML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MasterLibrary.ViewModel.CustomerVM.BuyBookVM
{
    public class DetailBookViewModel: BaseViewModel
    {
        #region Thuộc tính
        private int _Quantity;
        public int Quantity
        {
            get { return _Quantity; }
            set 
            { _Quantity = value;
                OnPropertyChanged();
            }
        }

        private decimal _TotalTien;
        public decimal TotalTien
        {
            get { return _TotalTien; }
            set
            {
                _TotalTien = value;
                OnPropertyChanged();
            }
        }

        private string _TotalTienStr;
        public string TotalTienStr
        {
            get { return _TotalTienStr; }
            set
            {
                _TotalTienStr = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region ICommand
        public ICommand FirstLoadML { get; set; }
        public ICommand CloseDetailBook { get; set; }
        public ICommand MinusCommand { get; set; }
        public ICommand PlusCommand { get; set; }
        public ICommand AddCart { get; set; }
        public ICommand BuyIt { get; set; }
        public ICommand QuantityChange { get; set; }

        #endregion

        #region 
        public static BookDTO selectBook { get; set; }
        
        #endregion

        public DetailBookViewModel()
        {
            // Load lần đầu
            FirstLoadML = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Quantity = 0;
                TotalTien = 0;
                TotalTienStr = Helper.FormatVNMoney(TotalTien);
            });

            // Đóng trang detailBook
            CloseDetailBook = new RelayCommand<object>((p) => { return true; }, (p) => 
            {
                DetailBook w = Application.Current.Windows.OfType<DetailBook>().FirstOrDefault();
                w.Close();
                BuyBookViewModel.MaskName.Visibility = Visibility.Collapsed;
            });

            // Giảm số lượng
            MinusCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                --Quantity;
            });

            // Tăng số lượng
            PlusCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ++Quantity;
            });

            // Thêm vào giỏ hàng
            AddCart = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MessageBox.Show("AddCart");
            });

            // Mua ngay
            BuyIt = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (Quantity == 0)
                {
                    MessageBoxML ms = new MessageBoxML("Thông báo", "Số lượng bằng 0 nên không thực hiện mua được", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    return;
                }

                decimal totalTien = TotalTien;

                List<BillDetailDTO> newbillDetailList = new List<BillDetailDTO>
                    {
                        new BillDetailDTO
                        {
                            MaSach = selectBook.MaSach,
                            SoLuong = Quantity,
                            GiaMoiCai = selectBook.Gia
                        }
                    };

                BillDTO bill = new BillDTO
                {
                    NGHD = DateTime.Now,
                    MaKH = MainCustomerViewModel.CurrentCustomer.MAKH,
                    TriGia = totalTien,
                };

                await BuyServices.Ins.CreateFullBill(bill, newbillDetailList);

            });

            // Thay đổi số lượng
            QuantityChange = new RelayCommand<Label>((p) => { return true; }, (p) =>
            {
                if (Quantity > selectBook.SoLuong)
                {
                    Quantity = selectBook.SoLuong;
                    p.Content = "Vượt số lượng hiện có";
                }
                if (Quantity <= 0) Quantity = 0;

                TotalTien = Quantity * selectBook.Gia;
                TotalTienStr = Helper.FormatVNMoney(TotalTien);
            });
        }
    }
}
