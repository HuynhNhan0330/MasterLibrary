using MasterLibrary.DTOs;
using MasterLibrary.Views.Admin.BorrowBookPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MasterLibrary.ViewModel.AdminVM.BorrowBookVM
{
    public partial class BorrowBookViewModel: BaseViewModel
    {
        #region Thuộc tính
        private bool _IsLoading;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { _IsLoading = value; OnPropertyChanged(); }
        }

        #endregion

        #region ICommand
        public ICommand MaskNameBrrowBook { get; set; }
        public ICommand LoadBorrowBookVorcherPage { get; set; }
        public ICommand LoadCollectionBookVorcherPage { get; set; }

        #endregion

        #region Thuộc tính tạm thời
        public Grid MaskName { get; set; }

        #endregion

        public BorrowBookViewModel()
        {
            #region BorrowBookViewModel
            MaskNameBrrowBook = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            LoadBorrowBookVorcherPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new BorrowBookVorcherPage();
            });

            LoadCollectionBookVorcherPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new CollectionBookVorcherPage();
            });

            #endregion

            #region BorrowBookVorcher
            FirstLoadBrrowBookVocher = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListBookInBorrow = new ObservableCollection<BookInBorrowDTO>();

                for (int i = 0; i < 10; ++i)
                {
                    ListBookInBorrow.Add(new BookInBorrowDTO
                    {
                        TenSach = "Dế mèn",
                        SoLuong = 10
                    });
                }
            });

            #endregion

            #region CollectionBookVorcher
            #endregion
        }
    }
}
