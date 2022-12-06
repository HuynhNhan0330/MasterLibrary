﻿using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Views.Customer;
using MasterLibrary.Views.Customer.BuyBookPage;
using MasterLibrary.Views.MessageBoxML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
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
        public ICommand UpdateInfo { get; set; }

        #endregion

        public SettingViewModel()
        {
            FirstLoadML = new RelayCommand<object> ((p) => { return true; }, async (p) => 
            {
                int MaKHcur = MainCustomerViewModel.CurrentCustomer.MAKH;
                Cus = await Task<CustomerDTO>.Run(() => CustormerServices.Ins.FindCustomer(MaKHcur));
                MaKH = Cus.MAKH;
                TenKH = Cus.TENKH;
                Email = Cus.EMAIL;
            });

            UpdateInfo = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                string match = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                Regex reg = new Regex(match);

                if (reg.IsMatch(Email) == false)
                {
                    MessageBoxML ms = new MessageBoxML("Thông báo", "Email không hợp lệ", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    return;
                }

                if (await Task<bool>.Run(() => CustormerServices.Ins.CheckEmailCustormer(Email, MaKH)))
                {
                    MessageBoxML ms = new MessageBoxML("Thông báo", "Email đã tồn tại", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    return;
                }

                if (await Task.Run(() => CustormerServices.Ins.updateCustomer(MaKH, TenKH, Email)))
                {
                    MessageBoxML ms = new MessageBoxML("Thông báo", "Chỉnh sửa thông tin thành công", MessageType.Accept, MessageButtons.OK);

                    MainCustomerWindow w = Application.Current.Windows.OfType<MainCustomerWindow>().FirstOrDefault();
                    w._CustomerName.Text = TenKH;

                    ms.ShowDialog();
                }
                else
                {
                    MessageBoxML ms = new MessageBoxML("Lỗi", "Xảy ra lỗi khi thực hiện thao tác", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                }
            });
        }
    }
}
