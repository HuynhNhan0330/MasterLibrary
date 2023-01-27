using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.ViewModel.CustomerVM;
using MasterLibrary.Views.Admin.BorrowBookPage;
using System;
using System.Collections.ObjectModel;
using System.Runtime.Remoting.Messaging;
using System.Windows;
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

        private bool _IsSaving;
        public bool IsSaving
        {
            get { return _IsSaving; }
            set { _IsSaving = value; OnPropertyChanged(); }
        }

        #endregion

        #region ICommand
        public ICommand MaskNameBrrowBook { get; set; }
        public ICommand LoadBorrowBookVorcherPage { get; set; }
        public ICommand LoadCollectionBookVorcherPage { get; set; }
        public ICommand ReSLCurrent { get; set; }

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
            FirstLoadBrrowBookVocherCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                FirstLoadBrrowBookVocher();
            });

            ReSLBookInBorrowCurrentCM = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                ReSLBookInBorrowCurrent(p);
            });

            MinusBookInBorrowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MinusBookInBorrow();
            });

            PlusBookInBorrowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                PlusBookInBorrow();
            });

            DeleteBookInBorrowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DeleteBookInBorrow();
            });

            FindNameCustomerCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                FindNameCustomer();
            });

            DeleteAllBookInBorrowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DeleteAllBookInBorrow();
            });

            AddBookToListBorrowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddBookToListBorrow();
            });

            OpenDetailBookCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenDetailBook();
            });

            BorrowAllBookCM = new RelayCommand<object>((p) => 
            {
                if (string.IsNullOrEmpty(TenKH) || ListBookInBorrow.Count <= 0)
                {
                    return false;
                }

                return true;
            },
            (p) =>
            {
                BorrowAllBook();
            });

            #endregion

            #region CollectionBookVorcher
            #endregion
        }


    }
}
