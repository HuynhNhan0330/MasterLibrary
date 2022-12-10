using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MasterLibrary.Views.Admin.BookManagePage
{
    /// <summary>
    /// Interaction logic for BookManagePage.xaml
    /// </summary>
    public partial class BookManagePage : Page
    {
        public BookManagePage()
        {
            InitializeComponent();
            Updating.IsEnabled = false;
            Deleting.IsEnabled = false;
        }

        //Không cho chọn sửa và xóa khi chưa chọn sách trên listview
        private void listview_managebook_MouseDown(object sender, RoutedEventArgs e)
        {
            Updating.IsEnabled = true;
            Deleting.IsEnabled = true;
        }

        #region Search
        private bool Filter(object item)
        {
            if (String.IsNullOrEmpty(txbFilter.Text))
                return true;
            else
                return ((item as BookDTO).TenSach.IndexOf(txbFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as BookDTO).TacGia.IndexOf(txbFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        
        public void CreateTextBoxFilter()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listview_managebook.ItemsSource);
            view.Filter = Filter;
        }

        private void TextBox_TextChanged_Find(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listview_managebook.ItemsSource).Refresh();
            CreateTextBoxFilter();
        }
        #endregion
    }
}
