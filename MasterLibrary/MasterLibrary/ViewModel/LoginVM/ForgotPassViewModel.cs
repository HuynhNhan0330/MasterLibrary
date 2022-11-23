using MasterLibrary.Views.LoginWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MasterLibrary.ViewModel.LoginVM
{
    public class ForgotPassViewModel: BaseViewModel
    {
        public ICommand CancelForgotPass { get; set; }

        public ForgotPassViewModel()
        {
            CancelForgotPass = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                LoginViewModel.MainFrame.Content = new LoginPage();
            });
        }
    }


}
