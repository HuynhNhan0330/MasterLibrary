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
        private ListView listview_managebook;
        private ObservableCollection<BookDTO> _listbookmanage;
        public ObservableCollection<BookDTO> Listbookmanage
        {
            get { return _listbookmanage; }
            set { _listbookmanage = value; OnPropertyChanged(); }
        }    

        #region ICommand

        public ICommand FirstLoadML { get; set; }
        public ICommand LoadStatisticalPageML { get; set; }
        public ICommand LoadBookManagerPageML { get; set; }
        public ICommand LoadHistoryPageML { get; set; }
        public ICommand LoadManageBookData { get; set; }
        public ICommand AddingBook { get; set; }
        public ICommand DeletingBook { get; set; }
        public ICommand UpdatingBook { get; set; }

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

            #region Page quản lý sách
            // Load dữ liệu vào trang quản lý sách
            LoadManageBookData = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                Loaded(p);
                listview_managebook = p;
            });

            //Nút thêm sách ở trang quản lý sách
            AddingBook = new RelayCommand<System.Windows.Controls.Button>((p) => { return true; }, (p) =>
            {
                AddingWindow add = new AddingWindow();
                add.ShowDialog();
                Loaded(listview_managebook);
            });

            //Nút xóa sách ở trang quản lý sách
            DeletingBook = new RelayCommand<System.Windows.Controls.Button>((p) => { return true; }, (p) =>
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
                        Loaded(listview_managebook);
                    }
                }
            });

            UpdatingBook = new RelayCommand<System.Windows.Controls.Button>((p) => { return true; }, (p) =>
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
                window.ShowDialog();
                Loaded(listview_managebook);
            });
            #endregion
        }

        //Load dữ liệu từ database vào listview của trang quản lý sách
        public void Loaded(ListView p)
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
            p.ItemsSource = Listbookmanage;
        }
    }
}
