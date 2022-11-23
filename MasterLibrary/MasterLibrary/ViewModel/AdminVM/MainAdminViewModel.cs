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

namespace MasterLibrary.ViewModel.AdminVM
{
    public partial class MainAdminViewModel: BaseViewModel
    {

        public ICommand FirstLoadML { get; set; }
        public ICommand LoadStatisticalPageML { get; set; }
        public ICommand LoadBookManagerPageML { get; set; }
        public ICommand LoadHistoryPageML { get; set; }

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
