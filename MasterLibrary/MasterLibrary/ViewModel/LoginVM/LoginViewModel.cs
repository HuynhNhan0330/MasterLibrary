using MasterLibrary.Views.LoginWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        public LoginViewModel()
        {
            LoadLoginPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame = p;
                p.Content = new LoginPage();
            });

            LoadForgotPassPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new ForgotPassPage();
            });

            LoadMask = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                Mask = p;
            });

            LoadRegister = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Window w1 = new RegisterWindow();
                
                //Mask.Visibility = Visibility.Visible;

                w1.ShowDialog();
            });
        }
        
    }
}
