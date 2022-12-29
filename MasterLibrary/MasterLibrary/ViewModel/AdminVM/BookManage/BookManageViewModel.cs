using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Views.Admin.BookManagePage;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Windows.Media.Imaging;
using MasterLibrary.Views.MessageBoxML;
using System.Collections.ObjectModel;
using MasterLibrary.DTOs;

namespace MasterLibrary.ViewModel.AdminVM
{
    public class BookManageViewModel : BaseViewModel
    {
        #region Property

        private ObservableCollection<BookDTO> _listbookmanage;
        public ObservableCollection<BookDTO> Listbookmanage
        {
            get { return _listbookmanage; }
            set { _listbookmanage = value; OnPropertyChanged(); }
        }
        private static ListView listview_tmp;

        private string _tensach;
        public string TenSach
        {
            get { return _tensach; }
            set { _tensach = value; OnPropertyChanged(); }
        }

        private string _tacgia;
        public string TacGia
        {
            get { return _tacgia; }
            set { _tacgia = value; OnPropertyChanged(); }
        }

        private string _nhaxuatban;
        public string NhaXuatBan
        {
            get { return _nhaxuatban; }
            set { _nhaxuatban = value; OnPropertyChanged(); }
        }

        private string _namxuatban;
        public string NamXuatBan
        {
            get { return _namxuatban; }
            set { _namxuatban = value; OnPropertyChanged(); }
        }

        private string _soluong;
        public string SoLuong
        {
            get { return _soluong; }
            set { _soluong = value; OnPropertyChanged(); }
        }

        private string _gia;
        public string Gia
        {
            get { return _gia; }
            set { _gia = value; OnPropertyChanged(); }
        }

        private string _theloai;
        public string TheLoai
        {
            get { return _theloai; }
            set { _theloai = value; OnPropertyChanged(); }
        }

        private string _mota;
        public string MoTa
        {
            get { return _mota; }
            set { _mota = value; OnPropertyChanged(); }
        }

        private string _tang;
        public string Tang
        {
            get { return _tang; }
            set { _tang = value; OnPropertyChanged(); }
        }

        private string _day;
        public string Day
        {
            get { return _day; }
            set { _day = value; OnPropertyChanged(); }
        }

        private string _imgsource;
        public string ImgSource
        {
            get { return _imgsource; }
            set { _imgsource = value; OnPropertyChanged(); }
        }
        #endregion
        public ICommand LoadManageBookData { get; set; }
        public ICommand SavingData { get; set; }
        public ICommand Updating { get; set; }
        public ICommand DeletingBook { get; set; }
        public ICommand UpdatingBook { get; set; }
        public ICommand ImportImageForAddingWindow { get; set; }
        public ICommand ImportImageForUpdatingWindow { get; set; }

        public BookManageViewModel()
        {
            //Nút update của chức năng chỉnh sửa
            Updating = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                using (var context = new MasterlibraryEntities())
                {
                    string connectionStr = context.Database.Connection.ConnectionString;
                    SqlConnection connect = new SqlConnection(connectionStr);
                    connect.Open();
                    SqlCommand command = new SqlCommand();

                    command.Connection = connect;
                    command.Parameters.AddWithValue("@masach", updatingwindow.masach);
                    command.Parameters.AddWithValue("@tensach", updatingwindow.TenSach.Text);
                    command.Parameters.AddWithValue("@tacgia", updatingwindow.TacGia.Text);
                    command.Parameters.AddWithValue("@namxb", updatingwindow.NamXuatBan.Text);
                    command.Parameters.AddWithValue("@nxb", updatingwindow.NhaXuatBan.Text);
                    command.Parameters.AddWithValue("@sl", updatingwindow.SoLuong.Text);
                    command.Parameters.AddWithValue("@gia", updatingwindow.Gia.Text);
                    command.Parameters.AddWithValue("@imagesource", updatingwindow.ImgSource.Text);
                    command.Parameters.AddWithValue("@theloai", updatingwindow.TheLoai.Text);
                    command.Parameters.AddWithValue("@mota", updatingwindow.MoTa.Text);
                    command.Parameters.AddWithValue("@tang", updatingwindow.Tang.Text);
                    command.Parameters.AddWithValue("@day", updatingwindow.Day.Text);

                    try
                    {
                        command.CommandText = "UPDATE SACH " +
                                              "SET TENSACH = @tensach, TACGIA = @tacgia, NAMXB = @namxb, NXB = @nxb, SL = @sl, GIA = @gia, IMAGESOURCE = @imagesource, THELOAI = @theloai, MOTA = @mota, VITRITANG = @tang, VITRIDAY = @day " +
                                              "WHERE MASACH = @masach";
                        context.SaveChanges();
                        int a = command.ExecuteNonQuery();
                        if (a != 0)
                        {
                            MessageBoxML msb = new MessageBoxML("Thông báo", "Cập nhật sách thành công", MessageType.Accept, MessageButtons.OK);
                            msb.ShowDialog();
                            p.Close();
                        }
                    }
                    catch
                    {
                        MessageBoxML msb = new MessageBoxML("Lỗi", "Vị trí không tồn tại", MessageType.Error, MessageButtons.OK);
                        msb.ShowDialog();
                    }
                }
                
            });

            // Nút import của chức năng sửa
            ImportImageForUpdatingWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.Filter = "JPG File (.jpg)|*.jpg";
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {
                    BitmapImage img = new BitmapImage();
                    img.BeginInit();
                    img.UriSource = new Uri(dlg.FileName);
                    img.EndInit();
                    updatingwindow.Image.Source = img;
                    Account account = new Account(
                    "dsrqapm0a",
                    "957237172661889",
                    "-1RSpajRMHkAQicQdFuyhIJfogE");

                    Cloudinary cloudinary = new Cloudinary(account);
                    cloudinary.Api.Secure = true;
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(dlg.FileName)
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);

                    updatingwindow.ImgSource.Text = uploadResult.Url.ToString();
                }
            });

            // Load dữ liệu vào trang quản lý sách
            LoadManageBookData = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                Loaded(p);
                listview_tmp = p;
            });

            //Nút xóa sách ở trang quản lý sách
            DeletingBook = new RelayCommand<System.Windows.Controls.MenuItem>((p) => { return true; }, (p) =>
            {
                BookDTO item = listview_tmp.Items[listview_tmp.SelectedIndex] as BookDTO;
                string masach = item.MaSach.ToString();
                using (var context = new MasterlibraryEntities())
                {
                    string connectionStr = context.Database.Connection.ConnectionString;
                    SqlConnection connect = new SqlConnection(connectionStr);
                    connect.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connect;
                    command.Parameters.AddWithValue("@masach", masach);
                    MessageBoxML msb = new MessageBoxML("Cảnh báo", "Bạn có muốn xóa sách này không", MessageType.Waitting, MessageButtons.YesNo);
                    
                    if (msb.ShowDialog() == true)
                    {
                        try
                        {
                            command.CommandText = "DELETE FROM SACH WHERE MASACH = @masach";
                            context.SaveChanges();
                            if (command.ExecuteNonQuery() != 0)
                            {
                                msb = new MessageBoxML("Thông báo", "Thành công", MessageType.Accept, MessageButtons.OK);
                                msb.ShowDialog();
                                Loaded(listview_tmp);
                            }
                        }
                        catch 
                        {
                            msb = new MessageBoxML("Thông báo", "Không thể xóa sách ", MessageType.Error, MessageButtons.OK);
                            msb.ShowDialog();
                        }
                    }
                }
            });

            UpdatingBook = new RelayCommand<System.Windows.Controls.MenuItem>((p) => { return true; }, (p) =>
            {
                BookDTO item = listview_tmp.Items[listview_tmp.SelectedIndex] as BookDTO;
                var masach = item.MaSach.ToString();
                updatingwindow window = new updatingwindow(masach);

                if (item.TenDay == null)
                {
                    window.TacGia_txb.Text = item.TacGia.ToString();
                    window.NhaXuatBan_txb.Text = item.NXB.ToString();
                    window.Gia_txb.Text = item.Gia.ToString();
                    window.TenSach_txb.Text = item.TenSach.ToString();
                }
                else 
                {
                    window.TenSach_txb.Text = item.TenSach.ToString();
                    window.TacGia_txb.Text = item.TacGia.ToString();
                    window.NhaXuatBan_txb.Text = item.NXB.ToString();
                    window.NamXuatBan_txb.Text = item.NamXB.ToString();
                    window.TheLoai_cbb.Text = item.TheLoai.ToString();
                    window.Gia_txb.Text = item.Gia.ToString();
                    window.Tang_txb.Text = item.TenTang;
                    window.Day_txb.Text = item.TenDay;
                    window.Source_txb.Text = item.ImageSource.ToString();
                    window.MoTa_txb.Text = item.MoTa.ToString();

                    //Load ảnh hiện tai lên trang chỉnh sửa
                    BitmapImage img = new BitmapImage();
                    img.BeginInit();
                    img.UriSource = new Uri(item.ImageSource);
                    img.EndInit();
                    window.image_img.Source = img;
                }
                window.ShowDialog();

                //load lại trang quản lý sách
                Loaded(listview_tmp);
            });
        }

        public void Loaded(ListView p)
        {
            Listbookmanage = new ObservableCollection<BookDTO>();
            using (var context = new MasterlibraryEntities())
            {
                foreach (var item in context.SACHes)
                {
                    BookDTO book = new BookDTO();
                    if(item.IMAGESOURCE == null)
                    {
                        book.MaSach = item.MASACH;
                        book.TenSach = item.TENSACH;
                        book.TacGia = item.TACGIA;
                        book.SoLuong = (int)item.SL;
                        book.Gia = (int)item.GIA;
                        book.NXB = item.NXB;
                    }
                    else
                    {
                        book.MaSach = item.MASACH;
                        book.TenSach = item.TENSACH;
                        book.TacGia = item.TACGIA;
                        book.SoLuong = (int)item.SL;
                        book.Gia = (int)item.GIA;
                        book.NXB = item.NXB;
                        book.NamXB = (int)item.NAMXB;
                        book.TheLoai = item.THELOAI;
                        book.ImageSource = item.IMAGESOURCE;
                        book.MoTa = item.MOTA;
                        book.TenDay = (from s in context.DAYKEs where s.MADAY == item.VITRIDAY select s.TENDAY).FirstOrDefault();
                        book.TenTang = (from s in context.TANGs where s.MATANG == item.VITRITANG select s.TENTANG).FirstOrDefault();
                    }    
                    Listbookmanage.Add(book);
                }
            }
            p.ItemsSource = Listbookmanage;
        }
    }
}
