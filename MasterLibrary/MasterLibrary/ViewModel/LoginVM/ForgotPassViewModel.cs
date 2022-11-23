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

<<<<<<< HEAD
=======
        #endregion


        #region Property
        private int Number;
        private string _mail;
        public string Mail
        {
            get { return _mail; }
            set { _mail = value; OnPropertyChanged(); }
        }

        private string _verificationnumber;
        public string Verificationnumber
        {
            get { return _verificationnumber; }
            set { _verificationnumber = value; OnPropertyChanged(); }
        }

        private string _newpass;
        public string Newpass
        {
            get { return _newpass; }
            set { _newpass = value; OnPropertyChanged(); }
        }

        private string _confirmnewpass;
        private string Confirmnewpass
        {
            get { return _confirmnewpass; }
            set { _confirmnewpass = value; OnPropertyChanged();}
        }
        #endregion
>>>>>>> hmlogin
        public ForgotPassViewModel()
        {
            CancelForgotPass = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                LoginViewModel.MainFrame.Content = new LoginPage();
            });
        }
    }
}
