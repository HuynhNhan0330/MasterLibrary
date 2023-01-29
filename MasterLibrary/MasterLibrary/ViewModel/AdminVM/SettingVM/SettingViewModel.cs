using MasterLibrary.Views.Admin.SettingPage;
using MasterLibrary.Views.Customer;
using MasterLibrary.Views.LoginWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Views.MessageBoxML;
using System.Reactive.Subjects;
using System.Text.RegularExpressions;
using MasterLibrary.Views.Admin;
using MasterLibrary.ViewModel.CustomerVM;

namespace MasterLibrary.ViewModel.AdminVM.SettingVM
{
    public class SettingViewModel : BaseViewModel
    {

        #region Property
        private bool Clicked = false;

        private Frame _frame;

        private Information _page;
        public Information myPage
        {
            get { return _page; }
            set { _page = value; OnPropertyChanged(); }
        }
        public Frame myFrame
        {
            get { return _frame; }
            set { _frame = value; OnPropertyChanged(); }
        }
        private int _maKH;
        public int MaKH
        {
            get { return _maKH; }
            set { _maKH = value; OnPropertyChanged(); }
        }

        private string _hoVaTen;
        public string HoVaTen
        {
            get { return _hoVaTen; }
            set { _hoVaTen = value; OnPropertyChanged();}
        }

        private string _tenTK;
        public string TenTK
        {
            get { return _tenTK; }
            set { _tenTK = value; OnPropertyChanged();}
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged();}
        }

        private string _CurrentPassword;
        public string CurrentPassword
        {
            get { return _CurrentPassword; }
            set { _CurrentPassword = value; OnPropertyChanged(); }
        }

        private string _NewPassword;
        public string NewPassword
        {
            get { return _NewPassword; }
            set { _NewPassword = value; OnPropertyChanged(); }
        }

        private string _ConfirmNewPassword;
        public string ConfirmNewPassword
        {
            get { return _ConfirmNewPassword; }
            set { _ConfirmNewPassword = value; OnPropertyChanged(); }
        }
        private bool _IsSaving;
        public bool IsSaving
        {
            get { return _IsSaving; }
            set { _IsSaving = value; OnPropertyChanged(); }
        }

        private string _passForChangingSomething;
        public string PassForChangingSomething
        {
            get { return _passForChangingSomething; }
            set { _passForChangingSomething = value; OnPropertyChanged(); }
        }

        private bool IsFixed = false;
        #endregion

        #region ICommand
        public ICommand LoadInforAccount { get; set; }
        public ICommand Loaded { get; set; }
        public ICommand Logout { get; set; }
        public ICommand LoadChangePass { get; set; }
        public ICommand CurrentPasswordChange { get; set; }
        public ICommand NewPasswordChange { get; set; }
        public ICommand ConfirmNewPasswordChange { get; set; }
        public ICommand SaveNewPasswordCommand { get; set; }
        public ICommand PassChanging { get; set; }
        public ICommand UpdateInforAdmin { get; set; }
        public ICommand Back { get; set; }
        public ICommand OK { get; set; }
        #endregion
        public SettingViewModel()
        {
            LoadInforAccount = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                Information w = new Information();
                p.Content = w;
                myPage = w;
            });


            Loaded = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MaKH = MasterLibrary.Models.DataProvider.AdminServices.MaNhanVien;
                HoVaTen = MasterLibrary.Models.DataProvider.AdminServices.TenNhanVien;
                TenTK = MasterLibrary.Models.DataProvider.AdminServices.UserNameNhanVien;
                Email = MasterLibrary.Models.DataProvider.AdminServices.EmailNhanVien;
                myFrame = p;
            });

            Logout = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                MainAdminWindow w = Application.Current.Windows.OfType<MainAdminWindow>().FirstOrDefault();
                w.Hide();

                LoginWindow nw = new LoginWindow();
                nw.Show();

                w.Close();
            });

            LoadChangePass = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                if (Clicked == false)
                {
                    p.Content = new ChangePass();
                    myPage.btnlogout.IsEnabled = false;
                    myPage.btnupdate.IsEnabled = false;
                    Clicked = true;
                }
                else
                {
                    p.Content = null;
                    Clicked = false;
                    myPage.btnlogout.IsEnabled = true;
                    myPage.btnupdate.IsEnabled = true;
                }
            });

            CurrentPasswordChange = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                CurrentPassword = p.Password;
            });

            NewPasswordChange = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                NewPassword = p.Password;
            });

            ConfirmNewPasswordChange = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                ConfirmNewPassword = p.Password;
            });

            SaveNewPasswordCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(CurrentPassword) ||
                    string.IsNullOrEmpty(NewPassword) ||
                    string.IsNullOrEmpty(ConfirmNewPassword))
                {
                    return false;
                }

                return true;
            }, async (p) =>
            {
                if (NewPassword != ConfirmNewPassword)
                {
                    MessageBoxML ms = new MessageBoxML("Thông báo", "Mật khẩu xác nhận không chính xác", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    return;
                }

                IsSaving = true;

                if (await Task<bool>.Run(() => AdminServices.Ins.ChangePassword(MaKH,NewPassword, CurrentPassword)))
                {
                    MessageBoxML ms = new MessageBoxML("Thông báo", "Đổi mật khẩu thành công", MessageType.Accept, MessageButtons.OK);
                    ms.ShowDialog();
                    myPage.btnlogout.IsEnabled = true;
                    myPage.btnupdate.IsEnabled = true;
                    Clicked = false;
                    myFrame.Content = null;

                }
                else
                {
                    MessageBoxML ms = new MessageBoxML("Thông báo", "Mật khẩu hiện tại không chính xác", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                }

                IsSaving = false;
            });

            PassChanging = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                PassForChangingSomething = p.Password;
            });

            UpdateInforAdmin = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                if (IsFixed == false)
                {
                    p.Visibility = Visibility.Visible;
                    myFrame.Content = null;
                    myPage.txb1.IsReadOnly = myPage.txb2.IsReadOnly = myPage.txb3.IsReadOnly = false;
                    myPage.btnlogout.IsEnabled = myPage.btnchangepass.IsEnabled = false;
                    IsFixed = true;
                }
                else
                {
                    p.Visibility = Visibility.Hidden;
                    myPage.txb1.IsReadOnly = myPage.txb2.IsReadOnly = myPage.txb3.IsReadOnly = true;
                    myFrame.Content = new ConfirmPass();
                    IsFixed = false;
                }    
            });

            Back = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                myFrame.Content = null;
                myPage.btnlogout.IsEnabled = myPage.btnchangepass.IsEnabled = true;
                Loaded.Execute(myFrame);
            });

            OK = new RelayCommand<Object>((p) => { return true; }, async (p) =>
            {
                if (await Task<bool>.Run(() => AdminServices.Ins.checkPass(MaKH, PassForChangingSomething)))
                {
                    string match = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                    Regex reg = new Regex(match);

                    if (reg.IsMatch(Email) == false)
                    {
                        MessageBoxML ms = new MessageBoxML("Thông báo", "Email không hợp lệ", MessageType.Error, MessageButtons.OK);
                        myPage.txb1.IsReadOnly = myPage.txb2.IsReadOnly = myPage.txb3.IsReadOnly = false;
                        ms.ShowDialog();
                        return;
                    }

                    IsSaving = true;

                    if (await Task<bool>.Run(() => AdminServices.Ins.CheckEmailAdmin(Email, MaKH)))
                    {
                        MessageBoxML ms = new MessageBoxML("Thông báo", "Email đã tồn tại", MessageType.Error, MessageButtons.OK);
                        myPage.txb1.IsReadOnly = myPage.txb2.IsReadOnly = myPage.txb3.IsReadOnly = false;
                        ms.ShowDialog();
                    }
                    else
                    if (await Task.Run(() => AdminServices.Ins.updateAdmin(MaKH, HoVaTen, Email, TenTK)))
                    {
                        MessageBoxML ms = new MessageBoxML("Thông báo", "Chỉnh sửa thông tin thành công", MessageType.Accept, MessageButtons.OK);
                        myPage.btnlogout.IsEnabled = myPage.btnchangepass.IsEnabled = true;
                        ms.ShowDialog();
                    }
                    else
                    {
                        MessageBoxML ms = new MessageBoxML("Lỗi", "Xảy ra lỗi khi thực hiện thao tác", MessageType.Error, MessageButtons.OK);
                        myPage.txb1.IsReadOnly = myPage.txb2.IsReadOnly = myPage.txb3.IsReadOnly = false;
                        ms.ShowDialog();
                    }

                    IsSaving = false;
                    myFrame.Content = null;

                }
                else
                {
                    MessageBoxML ms = new MessageBoxML("Thông báo", "Mật khẩu không chính xác", MessageType.Accept, MessageButtons.OK);
                    ms.ShowDialog();
                }
            });
        }
    }
}
