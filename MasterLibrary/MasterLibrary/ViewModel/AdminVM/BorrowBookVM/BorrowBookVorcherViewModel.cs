using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Views.MessageBoxML;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MasterLibrary.ViewModel.AdminVM.BorrowBookVM
{
    public partial class BorrowBookViewModel: BaseViewModel
    {
        #region Thuộc tính
        private ObservableCollection<BookInBorrowDTO> _ListBookInBorrow;
        public ObservableCollection<BookInBorrowDTO> ListBookInBorrow
        {
            get { return _ListBookInBorrow; }
            set { _ListBookInBorrow = value; OnPropertyChanged(); }
        }

        private BookInBorrowDTO _SelectedBookInBorrow;
        public BookInBorrowDTO SelectedBookInBorrow
        {
            get { return _SelectedBookInBorrow; }
            set { _SelectedBookInBorrow = value; OnPropertyChanged(); }
        }

        private int _MaKH;
        public int MaKH
        {
            get { return _MaKH; }
            set { _MaKH = value; OnPropertyChanged(); }
        }

        private string _TenKH;
        public string TenKH
        {
            get { return _TenKH; }
            set { _TenKH = value; OnPropertyChanged(); }
        }

        private DateTime _ExpirationDate;
        public DateTime ExpirationDate
        {
            get { return _ExpirationDate; }
            set { _ExpirationDate = value; OnPropertyChanged(); }
        }

        #endregion

        #region ICommand
        public ICommand FirstLoadBrrowBookVocherCM { get; set; }
        public ICommand ReSLBookInBorrowCurrentCM { get; set; }
        public ICommand MinusBookInBorrowCM { get; set; }
        public ICommand PlusBookInBorrowCM { get; set; }
        public ICommand DeleteBookInBorrowCM { get; set; }
        public ICommand FindNameCustomerCM { get; set; }
        public ICommand BorrowAllBookCM { get; set; }

        #endregion

        void FirstLoadBrrowBookVocher()
        {
            ListBookInBorrow = new ObservableCollection<BookInBorrowDTO>();

            ExpirationDate = DateTime.Now;
        }

        void FilterBookInBorrow()
        {
            ListBookInBorrow = new ObservableCollection<BookInBorrowDTO>(ListBookInBorrow);
        }

        void ReSLBookInBorrowCurrent(TextBox p)
        {
            BookInBorrowDTO BookInBorrowCurrent = SelectedBookInBorrow;

            if (BookInBorrowCurrent != null)
            {
                for (int i = 0; i < ListBookInBorrow.Count; i++)
                {
                    if (BookInBorrowCurrent.MaSach == ListBookInBorrow[i].MaSach)
                    {
                        if (BookInBorrowCurrent.SoLuong > ListBookInBorrow[i].SoLuongMax)
                        {
                            ListBookInBorrow[i].SoLuong = ListBookInBorrow[i].SoLuongMax;
                            FilterBookInBorrow();
                        }
                        else if (string.IsNullOrEmpty(p.Text))
                        {
                            ListBookInBorrow[i].SoLuong = 1;
                            FilterBookInBorrow();
                        }
                        break;
                    }
                }
            }
        }

        void MinusBookInBorrow()
        {
            BookInBorrowDTO BookInBorrowCurrent = SelectedBookInBorrow;

            if (BookInBorrowCurrent != null)
            {
                int positionBookInBorrowDelete = -1;

                for (int i = 0; i < ListBookInBorrow.Count; i++)
                {
                    if (BookInBorrowCurrent.MaSach == ListBookInBorrow[i].MaSach)
                    {
                        if (BookInBorrowCurrent.SoLuong > 1)
                        {
                            ListBookInBorrow[i].SoLuong -= 1;
                            FilterBookInBorrow();
                        }
                        else
                        {
                            MessageBoxML ms = new MessageBoxML("Thông báo", "Bạn muốn xoá?", MessageType.Error, MessageButtons.YesNo);

                            if (ms.ShowDialog() == true)
                            {
                                positionBookInBorrowDelete = i;
                            }
                        }
                        break;
                    }
                }

                if (positionBookInBorrowDelete != -1)
                {
                    ListBookInBorrow.RemoveAt(positionBookInBorrowDelete);
                    FilterBookInBorrow();
                }
            }
        }

        void PlusBookInBorrow()
        {
            BookInBorrowDTO BookInBorrowCurrent = SelectedBookInBorrow;

            if (BookInBorrowCurrent != null)
            {
                for (int i = 0; i < ListBookInBorrow.Count; i++)
                {
                    if (BookInBorrowCurrent.MaSach == ListBookInBorrow[i].MaSach)
                    {
                        if (BookInBorrowCurrent.SoLuong + 1 <= ListBookInBorrow[i].SoLuongMax)
                        {
                            ListBookInBorrow[i].SoLuong += 1;
                            FilterBookInBorrow();
                        }
                        break;
                    }
                }
            }
        }

        void DeleteBookInBorrow()
        {
            BookInBorrowDTO BookInBorrowCurrent = SelectedBookInBorrow;

            if (BookInBorrowCurrent != null)
            {
                int positionBookInBorrowDelete = -1;

                MessageBoxML ms = new MessageBoxML("Thông báo", "Bạn muốn xoá?", MessageType.Error, MessageButtons.YesNo);

                if (ms.ShowDialog() == true)
                {
                    for (int i = 0; i < ListBookInBorrow.Count; i++)
                    {
                        if (BookInBorrowCurrent.MaSach == ListBookInBorrow[i].MaSach)
                        {
                            positionBookInBorrowDelete = i;
                            break;
                        }
                    }

                    if (positionBookInBorrowDelete != -1)
                    {
                        ListBookInBorrow.RemoveAt(positionBookInBorrowDelete);
                        FilterBookInBorrow();
                    }
                }
            }
        }

        async void FindNameCustomer()
        {
            CustomerDTO CustomerCurrent = await Task.Run(() => CustormerServices.Ins.FindCustomer(MaKH));

            if (CustomerCurrent is null)
            {
                TenKH = "";
            }
            else
            {
                TenKH = CustomerCurrent.TENKH;
            }
        }

        void BorrowAllBook()
        {

        }
    }
}
