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
        private ObservableCollection<BookDTO> _listbookmanage;
        public ObservableCollection<BookDTO> Listbookmanage
        {
            get { return _listbookmanage; }
            set { _listbookmanage = value;}
        }
        private bool IsSort = false;
        public BookManagePage()
        {
            InitializeComponent();
            Updating.IsEnabled = false;
            Deleting.IsEnabled = false;
        }

        #region Sorting
        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader header = sender as GridViewColumnHeader;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listview_managebook.ItemsSource);
            if (IsSort)
            {
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription(header.Content.ToString(), ListSortDirection.Ascending));

            }
            else
            {
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription(header.Content.ToString(), ListSortDirection.Descending));
            }
            IsSort = !IsSort;
        }
        #endregion

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

        #region Tools

        // Nút thêm sách mới
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddingWindow add = new AddingWindow();
            add.ShowDialog();
            listview_managebook_Loaded(sender, e);
        }

        //Nút xóa sách
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            BookDTO item = listview_managebook.Items[listview_managebook.SelectedIndex] as BookDTO;
            string masach = item.MaSach.ToString();
            using (var context = new MasterlibraryEntities())
            {
                string connectionStr = context.Database.Connection.ConnectionString;
                SqlConnection connect = new SqlConnection(connectionStr);
                connect.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connect;
                command.Parameters.AddWithValue("@masach", masach);
                command.CommandText = "DELETE FROM SACH WHERE MASACH = @masach";
                context.SaveChanges();
                if (command.ExecuteNonQuery() != 0)
                {
                    MessageBox.Show("Xóa thành công");
                    listview_managebook_Loaded(sender, e);
                }
            }
        }

        //Nút sửa thông tin sách
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            BookDTO item = listview_managebook.Items[listview_managebook.SelectedIndex] as BookDTO;
            var masach = item.MaSach.ToString();
            updatingwindow window = new updatingwindow(masach);

            window.name_book_txb.Text = item.TenSach.ToString();
            window.name_aut_txb.Text = item.TacGia.ToString();
            window.name_nxb_txb.Text = item.NXB.ToString();
            window.namxb_txb.Text = item.NamXB.ToString();
            window.type_txb.Text = item.TheLoai.ToString();
            window.cost_txb.Text = item.Gia.ToString();
            window.tang_txb.Text = item.ViTriTang.ToString();
            window.day_txb.Text = item.ViTriDay.ToString();
            window.count_txb.Text = item.SoLuong.ToString();
            window.source_txb.Text = item.ImageSource.ToString();
            window.about_txb.Text = item.MoTa.ToString();
            window.Show();
            listview_managebook_Loaded(sender, e);
        }

        #endregion

        //Không cho chọn sửa và xóa khi chưa chọn sách trên listview
        private void listview_managebook_MouseDown(object sender, RoutedEventArgs e)
        {
            Updating.IsEnabled = true;           
            Deleting.IsEnabled = true;
        }

        //loaded
        private void listview_managebook_Loaded(object sender, RoutedEventArgs e)
        {
            Listbookmanage = new ObservableCollection<BookDTO>();
            using (var context = new MasterlibraryEntities())
            {
                foreach (var item in context.SACHes)
                {
                    BookDTO book = new BookDTO();
                    book.MaSach = item.MASACH;
                    book.TenSach = item.TENSACH;
                    book.TacGia = item.TACGIA;
                    book.NXB = item.NXB;
                    book.NamXB = (int)item.NAMXB;
                    book.TheLoai = item.THELOAI;
                    book.ImageSource = item.IMAGESOURCE;
                    book.SoLuong = (int)item.SL;
                    book.ViTriDay = (int)item.VITRIDAY;
                    book.ViTriTang = (int)item.VITRITANG;
                    book.Gia = (int)item.GIA;
                    book.MoTa = item.MOTA;
                    Listbookmanage.Add(book);
                }
            }
            listview_managebook.ItemsSource = Listbookmanage;
        }
    }
}
