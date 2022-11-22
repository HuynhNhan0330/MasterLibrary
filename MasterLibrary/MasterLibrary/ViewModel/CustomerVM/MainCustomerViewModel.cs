using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using MasterLibrary.Views.Customer.BuyBookPage;
using MasterLibrary.Views.Customer.BookLocationPage;

namespace MasterLibrary.ViewModel.CustomerVM
{
    public class MainCustomerViewModel
    {
        public ICommand FirstLoadML { get; set; }
        public ICommand LoadBuyBookPageML { get; set; }
        public ICommand LoadBookLocationPageML { get; set; }

        public MainCustomerViewModel()
        {
            LoadBuyBookPageML = new RelayCommand<Frame>((p) => { return true; }, (p) => 
            {
                p.Content = new BuyBookPage();
            });

            LoadBookLocationPageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new BookLocationPage();
            });
        }
    }
}
