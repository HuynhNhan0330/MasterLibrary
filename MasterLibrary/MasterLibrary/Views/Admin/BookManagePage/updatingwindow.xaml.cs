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
        private string masach;
        public updatingwindow()
        {
            InitializeComponent();
        }

        public updatingwindow(string a)
        {
            InitializeComponent();
            masach = a;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MasterlibraryEntities())
            {
                string connectionStr = context.Database.Connection.ConnectionString;
                SqlConnection connect = new SqlConnection(connectionStr);
                connect.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connect;
                command.Parameters.AddWithValue("@masach", masach);
                command.Parameters.AddWithValue("@tensach", name_book_txb.Text);
                command.Parameters.AddWithValue("@tacgia", name_aut_txb.Text);
                command.Parameters.AddWithValue("@namxb", namxb_txb.Text);
                command.Parameters.AddWithValue("@nxb", name_nxb_txb.Text);
                command.Parameters.AddWithValue("@sl", count_txb.Text);
                command.Parameters.AddWithValue("@gia", cost_txb.Text);
                command.Parameters.AddWithValue("@imagesource", source_txb.Text);
                command.Parameters.AddWithValue("@theloai", type_txb.Text);
                command.Parameters.AddWithValue("@mota", about_txb.Text);
                command.Parameters.AddWithValue("@tang", tang_txb.Text);
                command.Parameters.AddWithValue("@day", day_txb.Text);

                command.CommandText = "UPDATE SACH " +
                                      "SET TENSACH = @tensach, TACGIA = @tacgia, NAMXB = @namxb, NXB = @nxb, SL = @sl, GIA = @gia, IMAGESOURCE = @imagesource, THELOAI = @theloai, MOTA = @mota, VITRITANG = @tang, VITRIDAY = @day " +
                                      "WHERE MASACH = @masach";
                context.SaveChanges();
                int a = command.ExecuteNonQuery();
                if (a != 0)
                {
                    MessageBox.Show("Sửa thành công");
                    this.Close();
                }
            }
        }
    }
}
