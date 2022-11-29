using MasterLibrary.DTOs;
using MasterLibrary.Utils;
using MasterLibrary.Views.Customer.BuyBookPage;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MasterLibrary.ViewModel.CustomerVM.BuyBookVM
{
    public class DetailBookVM: BaseViewModel
    {
        #region Thuộc tính
        private int _Quantity;
        public int Quantity
        {
            get { return _Quantity; }
            set 
            { _Quantity = value;
                OnPropertyChanged();
            }
        }

        private decimal _TotalTien;
        public decimal TotalTien
        {
            get { return _TotalTien; }
            set
            {
                _TotalTien = value;
                OnPropertyChanged();
            }
        }

        private string _TotalTienStr;
        public string TotalTienStr
        {
            get { return _TotalTienStr; }
            set
            {
                _TotalTienStr = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region ICommand
        public ICommand FirstLoadML { get; set; }
        public ICommand CloseDetailBook { get; set; }
        public ICommand MinusCommand { get; set; }
        public ICommand PlusCommand { get; set; }
        public ICommand AddCart { get; set; }
        public ICommand BuyIt { get; set; }
        public ICommand QuantityChange { get; set; }

        #endregion

        public static BookDTO selectBook { get; set; }

        public DetailBookVM()
        {
            // Load lần đầu
            FirstLoadML = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Quantity = 0;
                TotalTien = 0;
                TotalTienStr = Helper.FormatVNMoney(TotalTien);
            });

            // Đóng trang detailBook
            CloseDetailBook = new RelayCommand<object>((p) => { return true; }, (p) => 
            {
                DetailBook w = Application.Current.Windows.OfType<DetailBook>().FirstOrDefault();
                w.Close();
                MainCustomerViewModel.MaskName.Visibility = Visibility.Collapsed;
            });

            // Giảm số lượng
            MinusCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                --Quantity;
            });

            // Tăng số lượng
            PlusCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ++Quantity;
            });

            // Thêm vào giỏ hàng
            AddCart = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MessageBox.Show("AddCart");
            });

            // Mua ngay
            BuyIt = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MessageBox.Show("BuyIt");
            });

            // Thay đổi số lượng
            QuantityChange = new RelayCommand<Label>((p) => { return true; }, (p) =>
            {
                if (Quantity > selectBook.SoLuong)
                {
                    Quantity = selectBook.SoLuong;
                    p.Content = "Vượt số lượng hiện có";
                }
                if (Quantity <= 0) Quantity = 0;

                TotalTien = Quantity * selectBook.Gia;
                TotalTienStr = Helper.FormatVNMoney(TotalTien);
            });

        }
    }
}
