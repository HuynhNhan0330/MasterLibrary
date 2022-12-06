using MasterLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MasterLibrary.ViewModel.CustomerVM.SettingVM
{
    public class SettingViewModel: BaseViewModel
    {
        #region Thuộc tính
        private CustomerDTO _Cus;
        public CustomerDTO Cus
        {
            get { return _Cus; }
            set { _Cus = value; OnPropertyChanged(); }
        }

        private int _MaKH;
        public int MaKH
        {
            get { return _MaKH; }
            set { _MaKH = value; OnPropertyChanged(); }
        }

        private string _TenKH;
        public string TenKH
        {
            get { return _TenKH; }
            set { _TenKH = value; OnPropertyChanged(); }
        }

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; OnPropertyChanged(); }
        }
        #endregion

        #region Icommand
        public ICommand FirstLoadML { get; set; }

        #endregion

        public SettingViewModel()
        {
            FirstLoadML = new RelayCommand<object> ((p) => { return true; }, (p) => 
            {
                int MaKH_ = MainCustomerViewModel.CurrentCustomer.MAKH;
                //Cus =       
            });
        }
    }
}
