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
using MasterLibrary.ViewModel.CustomerVM;

namespace MasterLibrary.ViewModel.LoginVM
{
    public class LoginViewModel: BaseViewModel
    {
        public Window loginW { get; set; }
        public static Frame MainFrame { get; set; }
        public static Grid Mask { get; set; }

        public ICommand LoadLoginPage { get; set; }
        public ICommand LoadForgotPassPage { get; set; }
        public ICommand LoadRegister { get; set; }
        public ICommand LoadVerificationPage { get; set; }

        public ICommand LoadMask { get; set; }
        public ICommand LoginML { get; set; }
        public ICommand PasswordChangedML { get; set; }
        public ICommand RegisterML { get; set; }
        public ICommand PasswordRegChangedML { get; set; }

        public ICommand SaveLoginWindowNameML { get; set; }

        #region property
        private string _usernamelog;
        public string Usernamelog
        {
            get { return _usernamelog; }
            set { _usernamelog = value; OnPropertyChanged(); }
        }
        private string _passwordlog;

        public string Passwordlog
        {
            get { return _passwordlog; }
            set { _passwordlog = value; OnPropertyChanged(); }
        }

        private string _fullnamereg;
        public string Fullnamereg
        {
            get { return _fullnamereg; }
            set { _fullnamereg = value; OnPropertyChanged(); }
        }

        private string _emailreg;
        public string Emailreg
        {
            get { return _emailreg; }
            set { _emailreg = value; OnPropertyChanged(); }
        }

        private string _usernamereg;
        public string Usernamereg
        {
            get { return _usernamereg; }
            set { _usernamereg = value; OnPropertyChanged(); }
        }
        private string _passwordreg;
        public string Passwordreg
        {
            get { return _passwordreg; }
            set { _passwordreg = value;OnPropertyChanged(); }
        }
        #endregion
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

                Mask.Visibility = Visibility.Visible;

                w1.ShowDialog();
            });

            // Bật page xác thực
            LoadVerificationPage = new RelayCommand<Label>((p) => { return true; }, async (p) =>
            {
                MainFrame.Content = new VerificationPage();
            });

            // Lưu widow login
            SaveLoginWindowNameML = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                loginW = p;   
            });

            #region Login
            // Thực hiện đăng nhập tài khoản
            LoginML = new RelayCommand<Label>((p) => { return true; }, async (p) =>
            {
                string username = Usernamelog;
                string password = Passwordlog;

                // thực hiện đăng nhập
                checkValidateAccount(username, password, p);
            });

            // Nhận mật khẩu mỗi lần thay đổi
            PasswordChangedML = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Passwordlog = p.Password;
            });
            #endregion

            #region Register
            RegisterML = new RelayCommand<Label>((p) => { return true; }, async (p) =>
            {
                string fullname = Fullnamereg;
                string email = Emailreg;
                string usernamereg = Usernamereg;
                string passwordreg = Passwordreg;

                //thực hiện đăng ký tài khoản
                CustormerServices.Ins.Register(fullname, email, usernamereg, passwordreg);
            });

            // Nhận mật khẩu mỗi lần thay đổi
            PasswordRegChangedML = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Passwordreg = p.Password;
            });

            #endregion
        }

        public async Task checkValidateAccount(string usr, string pwd, Label lbl)
        {
            if (string.IsNullOrEmpty(usr) || string.IsNullOrEmpty(pwd))
            {
                lbl.Content = "Sai tài khoản hoặc mật khẩu";
                return;
            }

            // Thực hiện Login tài khoản customer
            (bool loginCus, string messCus, CustomerDTO cus) = await Task<(bool loginSuccess, string message, CustomerDTO cus)>.Run(() => CustormerServices.Ins.Login(usr, pwd));

            // thực hiện Login tài khoản admin
            (bool loginAdmin, string messAdmin) = await Task<(bool loginSuccess, string message)>.Run(() => AdminServices.Ins.Login(usr, pwd));

            if (loginCus)
            {
                
                MainCustomerWindow w1 = new MainCustomerWindow();
                MainCustomerViewModel.CurrentCustomer = cus;
                w1._CustomerName.Text = cus.TENKH;
                w1.Show();
                loginW.Close();
            }
            else if (loginAdmin)
            {
                MainAdminWindow w1 = new MainAdminWindow();
                w1.Show();
                loginW.Close();
            }
            else
            {
                lbl.Content = messCus;
            }
        }

    }
}
