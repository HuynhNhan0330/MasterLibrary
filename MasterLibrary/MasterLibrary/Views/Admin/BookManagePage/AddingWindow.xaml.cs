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
    /// Interaction logic for AddingWindow.xaml
    /// </summary>
    public partial class AddingWindow : Window
    {
        public AddingWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (name_book_txb.Text != "" && name_aut_txb.Text != "" && namxb_txb.Text != "" && name_nxb_txb.Text != "" && count_txb.Text != "" &&
               cost_txb.Text != "" && source_txb.Text != "" && about_txb.Text != "" && tang_txb.Text != "" && day_txb.Text != "" && type_txb.Text != "")
            {
                using (var context = new MasterlibraryEntities())
                {
                    string connectionStr = context.Database.Connection.ConnectionString;
                    SqlConnection connect = new SqlConnection(connectionStr);
                    connect.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connect;
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

                    try
                    {
                        command.CommandText = "INSERT INTO SACH(TENSACH, TACGIA, NAMXB, NXB, SL, GIA, IMAGESOURCE, THELOAI, MOTA, VITRITANG, VITRIDAY) VALUES(@tensach, @tacgia, @namxb, @nxb, @sl, @gia, @imagesource, @theloai, @mota, @tang, @day)";
                        context.SaveChanges();
                        int a = command.ExecuteNonQuery();
                        if(a != 0)
                        {
                            MessageBox.Show("Thêm thành công");
                            this.Close();
                        }    
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Vị trí không tồn tại trong hệ thống");
                    }
                }
            }
            else
            {
                if (name_book_txb.Text == "")
                    Error1.Content = "*Không được bỏ trống.";
                else
                    Error1.Content = "";


                if (name_aut_txb.Text == "")
                    Error2.Content = "*Không được bỏ trống.";
                else
                    Error2.Content = "";


                if (name_nxb_txb.Text == "")
                    Error3.Content = "*Không được bỏ trống.";
                else
                    Error3.Content = "";


                if (namxb_txb.Text == "")
                    Error4.Content = "*Không được bỏ trống.";
                else
                    Error4.Content = "";


                if (count_txb.Text == "")
                    Error5.Content = "*Không được bỏ trống.";
                else
                    Error5.Content = "";


                if (cost_txb.Text == "")
                    Error6.Content = "*Không được bỏ trống.";
                else
                    Error6.Content = "";


                if (type_txb.Text == "")
                    Error7.Content = "*Chọn 1 trong các thể loại.";
                else
                    Error7.Content = "";


                if (about_txb.Text == "")
                    Error8.Content = "*Không được bỏ trống.";
                else
                    Error8.Content = "";


                if (tang_txb.Text == "")
                    Error9.Content = "*Không được bỏ trống.";
                else
                    Error9.Content = "";


                if (day_txb.Text == "")
                    Error10.Content = "*Không được bỏ trống.";
                else
                    Error10.Content = "";

                if (source_txb.Text == "")
                    Error11.Content = "*Không được bỏ trống.";
                else
                    Error11.Content = "";

                addingwindow_Loaded(sender, e);
            }
        }

        private void addingwindow_Loaded(object sender, RoutedEventArgs e) { }
    }
}
