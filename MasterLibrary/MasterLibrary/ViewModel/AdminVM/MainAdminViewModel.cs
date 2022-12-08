using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using MasterLibrary.Views.Admin.StatisticalPage;
using MasterLibrary.Views.Admin.BookManagePage;
using MasterLibrary.Views.Admin.HistoryPage;
using MasterLibrary.Models.DataProvider;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.ObjectModel;
using MasterLibrary.DTOs;
using System.Web.UI.WebControls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.ComponentModel;

namespace MasterLibrary.ViewModel.AdminVM
{
    public partial class MainAdminViewModel : BaseViewModel
    {
        
        private ObservableCollection<BookDTO> _listbookmanage;
        public ObservableCollection<BookDTO> Listbookmanage
        {
            get { return _listbookmanage; }
            set { _listbookmanage = value; OnPropertyChanged(); }
        }    

        #region Command

        public ICommand FirstLoadML { get; set; }
        public ICommand LoadStatisticalPageML { get; set; }
        public ICommand LoadBookManagerPageML { get; set; }
        public ICommand LoadHistoryPageML { get; set; }
        public ICommand LoadManageBookData { get; set; }
        public ICommand AddingBook { get; set; }
        #endregion
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
        }
    }
}
