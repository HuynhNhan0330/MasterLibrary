using MasterLibrary.ConnectDatabase;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace MasterLibrary.View
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        public login()
        {
            InitializeComponent();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void revealModeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            string s = PasswordBox.Password.Trim();
            
        }

        private void revealModeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBox.PasswordChar = '●';
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Register register = new Register();
            register.Show();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            if (!AllowLogin())
                return;
            DataTable dt = ConnectDatabase.Dataconfig.DataTransport("SELECT * FROM UserInf");
            bool flag = false;
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                if (NameTextBox.Text == Convert.ToString(dt.Rows[i]["Username"]) && PasswordBox.Password.Trim() == Convert.ToString(dt.Rows[i]["Password"]))
                {
                    MessageBox.Show("Login successful!","", MessageBoxButton.OK, MessageBoxImage.None);
                    flag = true;
                    break;
                }
            }
            if (!flag)
                MessageBox.Show("Login unsuccessful", "",MessageBoxButton.OK, MessageBoxImage.Hand);
        }

        private bool AllowLogin()
        {
            if(NameTextBox.Text.Length == 0)
            {
                MessageBox.Show("Fill in your user name", "Warning",MessageBoxButton.OK, MessageBoxImage.Warning);
                NameTextBox.Focus();
                return false;
            }
            if(PasswordBox.Password.Trim().Length == 0)
            {
                MessageBox.Show("Fill in your password", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                NameTextBox.Focus();
                return false;
            }
            return true;
        }

        
    }
}
