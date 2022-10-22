using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void buttonregister_Click(object sender, RoutedEventArgs e)
        {
            if (!AllowRegister())
                return;
            string sql = "INSERT INTO UserInf VALUES (" + "'" + usernametextbox.Text + "'" + "," + "'" + PasswordBox.Password.ToString() + "'" + "," + "'" + emailtextbox.Text + "'" + ")";
            if (ConnectDatabase.Dataconfig.DataExcution(sql) != 0)
                MessageBox.Show("Register successful!");
            else
                MessageBox.Show("Register unsuccessful");
        }
        private bool AllowRegister()
        {
            if (emailtextbox.Text.Length == 0)
            {
                MessageBox.Show("Fill in your email", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                emailtextbox.Focus();
                return false;
            }
            if (usernametextbox.Text.Length == 0)
            {
                MessageBox.Show("Fill in your user name", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                usernametextbox.Focus();
                return false;
            }
            if (PasswordBox.Password.Trim().Length == 0)
            {
                MessageBox.Show("Fill in your password", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                PasswordBox.Focus();
                return false;
            }
            if(ConFPasswordBox.Password.Trim().Length == 0)
            {
                MessageBox.Show("Confirm your password", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                ConFPasswordBox.Focus();
                return false;
            }     
            return true;
        }
    }
}
