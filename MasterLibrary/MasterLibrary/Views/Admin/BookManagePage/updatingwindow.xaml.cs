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

        public updatingwindow()
        {
            InitializeComponent();
        }

        public updatingwindow(string a)
        {
            InitializeComponent();
            masach = a;
            TenSach = name_book_txb; TacGia = name_aut_txb; NamXuatBan = namxb_txb; NhaXuatBan = name_nxb_txb; SoLuong = count_txb;
            Gia = cost_txb; ImgSource = source_txb; TheLoai = type_txb; MoTa = about_txb; Day = day_txb; Tang = tang_txb;
        }
    }
}
