using MasterLibrary.Views.LoginWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MasterLibrary.ViewModel.LoginVM
{
    public class LoginViewModel: BaseViewModel
    {
        public static Frame MainFrame { get; set; }
        public ICommand LoadLoginPage { get; set; }
        public ICommand ForgotPassCM { get; set; }

        public LoginViewModel()
        {
            LoadLoginPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame = p;
                p.Content = new LoginPage();
            });

            ForgotPassCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new ForgotPassPage();
            });
        }
        
    }


}
