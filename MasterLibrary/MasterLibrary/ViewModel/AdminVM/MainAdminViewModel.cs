using System;
using System.Windows.Controls;
using System.Windows.Input;
using MasterLibrary.Views.Admin.StatisticalPage;
using MasterLibrary.Views.Admin.BookManagePage;
using MasterLibrary.Views.Admin.HistoryPage;
using MasterLibrary.Models.DataProvider;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.ObjectModel;
using MasterLibrary.DTOs;
using System.Windows.Media.Imaging;
using MasterLibrary.Views.Admin.ImportBookPage;
using MasterLibrary.Views.Admin.LocationPage;
using System.Windows.Media;
using MasterLibrary.Views.MessageBoxML;
using System.Globalization;
using MasterLibrary.Views.Admin.TroublePage;
using MasterLibrary.Views.Admin.BorrowBookPage;

namespace MasterLibrary.ViewModel.AdminVM
{
    public partial class MainAdminViewModel : BaseViewModel
    {
        

        public ICommand FirstLoadML { get; set; }
        public ICommand LoadStatisticalPageML { get; set; }
        public ICommand LoadBookManagerPageML { get; set; }
        public ICommand LoadHistoryPageML { get; set; }
        public ICommand LoadImportBookPageML { get; set; }
        public ICommand LoadLocationPageML { get; set; }
        
        public ICommand LoadBorrowBookPageML { get; set; }
        public ICommand LoadTroublePageML { get; set; }



        public MainAdminViewModel()
        {
            // Load trang phân tích
            LoadStatisticalPageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new StatisticalPage();
            });

            // Load trang quản lí sách
            LoadBookManagerPageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new BookManagePage();
            });

            // Load trang lịch sử
            LoadHistoryPageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new HistoryPage();
            });

            // Load trang nhập sách
            LoadImportBookPageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new ImportBookPage();
            });

            // Load trang tầng dẫy
            LoadLocationPageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new LocationPage();
            });


            // Load trang thuê sách
            LoadBorrowBookPageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new BorrowBookPage();
            });

            // Load trang sự cố
            LoadTroublePageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new TroublePage();
            });

        }  
    }
}
