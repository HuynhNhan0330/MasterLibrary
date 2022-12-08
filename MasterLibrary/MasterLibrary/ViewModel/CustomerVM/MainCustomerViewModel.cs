﻿using System.Windows.Controls;
using System.Windows.Input;
using MasterLibrary.Views.Customer.BuyBookPage;
using MasterLibrary.Views.Customer.BookLocationPage;
using MasterLibrary.DTOs;
using MasterLibrary.Views.Customer.SettingPage;
using System.Windows;
using MasterLibrary.Views.LoginWindow;

namespace MasterLibrary.ViewModel.CustomerVM
{
    public class MainCustomerViewModel: BaseViewModel
    {
        #region Thuộc tính
        private static CustomerDTO _CurrentCustomer;
        public static CustomerDTO CurrentCustomer
        {
            get { return _CurrentCustomer; }
            set
            {
                _CurrentCustomer = value;
            }
        }
        #endregion

        #region ICommand
        public ICommand FirstLoadCustomer { get; set; }
        public ICommand LoadBuyBookPageML { get; set; }
        public ICommand TurnOnBuyBook { get; set; }
        public ICommand LoadBookLocationPageML { get; set; }
        public ICommand TurnOnBookLocation { get; set; }
        public ICommand LoadSettingPageML { get; set; }
        public ICommand TurnOnSetting { get; set; }
        public ICommand SignOutML { get; set; }
        #endregion

        public MainCustomerViewModel()
        {
            // Load trang mua sách
            LoadBuyBookPageML = new RelayCommand<Frame>((p) => { return true; }, async (p) => 
            {
                p.Content = new BuyBookPage();
            });

            // Load trang vị trí sách
            LoadBookLocationPageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new BookLocationPage();
            });

            // Load trang cài đặt
            LoadSettingPageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new SettingPage();
            });

            // Bật button mua sách
            TurnOnBuyBook = new RelayCommand<RadioButton>((p) => { return true; }, (p) =>
            {
                p.IsChecked = true;
            });

            // Bật button vị trí sách
            TurnOnBookLocation = new RelayCommand<RadioButton>((p) => { return true; }, (p) =>
            {
                p.IsChecked = true;
            });

            // Bật button setting
            TurnOnSetting = new RelayCommand<RadioButton>((p) => { return true; }, (p) =>
            {
                p.IsChecked = true;
            });

            // Đăng xuất
            SignOutML = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();

                LoginWindow w = new LoginWindow();
                w.Show();

                p.Close();
            });
        }
    }
}
