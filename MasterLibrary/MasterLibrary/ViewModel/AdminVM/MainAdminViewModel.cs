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
        private string _textHeading;
        public string textHeading
        {
            get { return _textHeading; }
            set { _textHeading = value; OnPropertyChanged(); }
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
                textHeading = "Thống kê doanh thu";
                p.Content = new StatisticalPage();
            });

            // Load trang quản lí sách
            LoadBookManagerPageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                textHeading = "Quản lý sách";
                p.Content = new BookManagePage();
            });

            // Load trang lịch sử
            LoadHistoryPageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                textHeading = "Lịch sử giao dịch";
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
            DeletingBook = new RelayCommand<System.Windows.Controls.MenuItem>((p) => { return true; }, (p) =>
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
                    if(MessageBox.Show("Bạn có muốn xóa sách " + item.TenSach + "?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            command.CommandText = "DELETE FROM SACH WHERE MASACH = @masach";
                            context.SaveChanges();
                            if (command.ExecuteNonQuery() != 0)
                            {
                                MessageBox.Show("Xóa thành công");
                                Loaded(listview_managebook);
                            }
                        }
                        catch { MessageBox.Show("Không thể xóa sách"); }
                    }    
                }
            });

            UpdatingBook = new RelayCommand<System.Windows.Controls.MenuItem>((p) => { return true; }, (p) =>
            {
                BookDTO item = listview_managebook.Items[listview_managebook.SelectedIndex] as BookDTO;
                var masach = item.MaSach.ToString();
                updatingwindow window = new updatingwindow(masach);

                window.TenSach_txb.Text = item.TenSach.ToString();
                window.TacGia_txb.Text = item.TacGia.ToString();
                window.NhaXuatBan_txb.Text = item.NXB.ToString();
                window.NamXuatBan_txb.Text = item.NamXB.ToString();
                window.TheLoai_cbb.Text = item.TheLoai.ToString();
                window.Gia_txb.Text = item.Gia.ToString();
                window.Tang_txb.Text = item.ViTriTang.ToString();
                window.Day_txb.Text = item.ViTriDay.ToString();
                window.SoLuong_txb.Text = item.SoLuong.ToString();
                window.Source_txb.Text = item.ImageSource.ToString();
                window.MoTa_txb.Text = item.MoTa.ToString();

                //Load ảnh hiện tai lên trang chỉnh sửa
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(item.ImageSource);
                img.EndInit();
                window.image_img.Source = img;
                window.ShowDialog();

                //load lại trang quản lý sách
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
