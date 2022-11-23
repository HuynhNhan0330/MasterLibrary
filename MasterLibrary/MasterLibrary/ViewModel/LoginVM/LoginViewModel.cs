using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Views.LoginWindow;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MasterLibrary.Views.Admin;
using MasterLibrary.Views.Customer;
using MaterialDesignThemes.Wpf;

namespace MasterLibrary.ViewModel.LoginVM
{
    public class LoginViewModel: BaseViewModel
    {
        public static Frame MainFrame { get; set; }
        public static Grid Mask { get; set; }

        public ICommand LoadLoginPage { get; set; }
        public ICommand LoadForgotPassPage { get; set; }
        public ICommand LoadRegister { get; set; }
        public ICommand LoadMask { get; set; }
        public ICommand LoginML { get; set; }
        public ICommand PasswordChangedML { get; set; }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        public LoginViewModel()
        {
            // Load page đăng nhập
            LoadLoginPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame = p;
                p.Content = new LoginPage();
            });

            // Load page quên mật khẩu
            LoadForgotPassPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new ForgotPassPage();
            });

            // Load mặt nạ làm mở window hiện tại khi mở window khác
            LoadMask = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                Mask = p;
            });

            // Bật window đăng kí
            LoadRegister = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Window w1 = new RegisterWindow();
                
                //Mask.Visibility = Visibility.Visible;

                w1.ShowDialog();
            });

            // Thực hiện đăng nhập tài khoản
            LoginML = new RelayCommand<Label>((p) => { return true; }, async (p) =>
            {
                string username = Username;
                string password = Password;

                // thực hiện đăng nhập
                checkValidateAccount(username, password, p);
            });

            // Nhận mật khẩu mỗi lần thay đổi
            PasswordChangedML = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Password = p.Password;
            });
        }

        public async Task checkValidateAccount(string usr, string pwd, Label lbl)
        {
            if (string.IsNullOrEmpty(usr) || string.IsNullOrEmpty(pwd))
            {
                lbl.Content = "Vui lòng nhập đủ thông tin";
                return;
            }

            // Thực hiện Login tài khoản customer
            (bool loginCus, string messCus, CustomerDTO cus) = await Task<(bool loginSuccess, string message, CustomerDTO cus)>.Run(() => CustormerServices.Ins.Login(usr, pwd));
            
            // thực hiện Login tài khoản admin
            (bool loginAdmin, string messAdmin) = await Task<(bool loginSuccess, string message)>.Run(() => AdminServices.Ins.Login(usr, pwd));

            if (loginCus)
            {
                //Password = "";
                //LoginWindow.Hide();
                MainCustomerWindow w1 = new MainCustomerWindow();
                w1.Show();
                //LoginWindow.Close();
            } 
            else if (loginAdmin)
            {
                //LoginWindow.Hide();
                MainAdminWindow w1 = new MainAdminWindow();
                w1.Show();
                //LoginWindow.Close();
            }
            else
            {
                lbl.Content = messCus;
            }
        }
    }


}
