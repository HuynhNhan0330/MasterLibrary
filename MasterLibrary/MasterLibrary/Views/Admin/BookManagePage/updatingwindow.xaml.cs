using MasterLibrary.Models.DataProvider;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MasterLibrary.Views.Admin.BookManagePage
{
    /// <summary>
    /// Interaction logic for updatingwindow.xaml
    /// </summary>
    public partial class updatingwindow : Window
    {
        public static string masach;
        public static TextBox TenSach;
        public static TextBox TacGia;
        public static TextBox NamXuatBan;
        public static TextBox NhaXuatBan;
        public static TextBox SoLuong;
        public static TextBox Gia;
        public static TextBox ImgSource;
        public static ComboBox TheLoai;
        public static TextBox MoTa;
        public static TextBox Tang;
        public static TextBox Day;
        public static Image Image;

        public updatingwindow()
        {
            InitializeComponent();
        }

        public updatingwindow(string a)
        {
            InitializeComponent();
            masach = a;
            TenSach = TenSach_txb; TacGia = TacGia_txb; NamXuatBan = NamXuatBan_txb; NhaXuatBan = NhaXuatBan_txb; SoLuong = SoLuong_txb;
            Gia = Gia_txb; ImgSource = Source_txb; TheLoai = TheLoai_cbb; MoTa = MoTa_txb; Day = Day_txb; Tang = Tang_txb;
            Image = image_img;
        }
    }
}
