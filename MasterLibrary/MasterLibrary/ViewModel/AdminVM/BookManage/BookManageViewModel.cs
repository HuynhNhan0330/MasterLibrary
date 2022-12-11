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

namespace MasterLibrary.ViewModel.AdminVM
{
    public class BookManageViewModel : BaseViewModel
    {
        #region Property

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
        private ICommand _ILoaded;
        public ICommand ILoaded
        {
            get { return _ILoaded; }
            set { _ILoaded = value; OnPropertyChanged(); }
        }
        public ICommand SavingData { get; set; }
        public ICommand Updating { get; set; }
        public ICommand ImportImageForAddingWindow { get; set; }
        public ICommand ImportImageForUpdatingWindow { get; set; }

        public BookManageViewModel()
        {

            #region Nút save của chức năng thêm
            SavingData = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (TenSach != "" && TacGia != "" && NamXuatBan != "" && NhaXuatBan != "" && SoLuong != "" &&
               Gia != "" && AddingWindow.ImgSource.Text != "" && MoTa != "" && Tang != "" && Day != "" && TheLoai != "")
                {
                    using (var context = new MasterlibraryEntities())
                    {
                        string connectionStr = context.Database.Connection.ConnectionString;
                        SqlConnection connect = new SqlConnection(connectionStr);
                        connect.Open();
                        SqlCommand command = new SqlCommand();
                        command.Connection = connect;

                        command.Parameters.AddWithValue("@tensach", TenSach);
                        command.Parameters.AddWithValue("@tacgia", TacGia);
                        command.Parameters.AddWithValue("@namxb", NamXuatBan);
                        command.Parameters.AddWithValue("@nxb", NhaXuatBan);
                        command.Parameters.AddWithValue("@sl", SoLuong);
                        command.Parameters.AddWithValue("@gia", Gia);
                        command.Parameters.AddWithValue("@imagesource", AddingWindow.ImgSource.Text);
                        command.Parameters.AddWithValue("@theloai", TheLoai);
                        command.Parameters.AddWithValue("@mota", MoTa);
                        command.Parameters.AddWithValue("@tang", Tang);
                        command.Parameters.AddWithValue("@day", Day);

                        try
                        {
                            command.CommandText = "INSERT INTO SACH(TENSACH, TACGIA, NAMXB, NXB, SL, GIA, IMAGESOURCE, THELOAI, MOTA, VITRITANG, VITRIDAY) VALUES(@tensach, @tacgia, @namxb, @nxb, @sl, @gia, @imagesource, @theloai, @mota, @tang, @day)";
                            context.SaveChanges();
                            int a = command.ExecuteNonQuery();
                            if (a != 0)
                            {
                                MessageBox.Show("Thêm thành công");
                                p.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Vị trí không tồn tại trong hệ thống");
                        }
                    }
                }
                else
                    MessageBox.Show("Điền đầy đủ thông tin");
            });
            #endregion

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
                            MessageBox.Show("Sửa thành công");
                            p.Close();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Vị trí không tồn tại");
                    }
                }
                
            });

            // Nút import của chức năng thêm
            ImportImageForAddingWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
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
                    AddingWindow.Image.Source = img;
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
                AddingWindow.ImgSource.Text = uploadResult.Url.ToString();
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
        }
    }
}
