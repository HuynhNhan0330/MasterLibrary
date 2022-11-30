using System.Windows.Controls;
using System.Windows.Input;
using MasterLibrary.Views.Customer.BuyBookPage;
using MasterLibrary.Views.Customer.BookLocationPage;
using MasterLibrary.DTOs;
using System.Collections.ObjectModel;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Utils;
using MasterLibrary.ViewModel.CustomerVM.BuyBookVM;
using MasterLibrary.Views.Customer.SettingPage;
using System.Windows;
using MasterLibrary.Views.Customer;
using System.Linq;
using MasterLibrary.Views.LoginWindow;
using MasterLibrary.ViewModel.LoginVM;

namespace MasterLibrary.ViewModel.CustomerVM
{
    public partial class MainCustomerViewModel: BaseViewModel
    {
        #region Thuộc tính
        private ObservableCollection<BookDTO> _ListBook;
        public ObservableCollection<BookDTO> ListBook
        {
            get { return _ListBook; }
            set { _ListBook = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BookDTO> _ListBook1;
        public ObservableCollection<BookDTO> ListBook1
        {
            get { return _ListBook1; }
            set { _ListBook1 = value; OnPropertyChanged(); }
        }
        
        private ObservableCollection<string> _GenreBook;
        public ObservableCollection<string> GenreBook
        {
            get { return _GenreBook; }
            set { _GenreBook = value; OnPropertyChanged(); }
        }

        private string _SelectedGenre;
        public string SelectedGenre
        {
            get { return _SelectedGenre; }
            set { _SelectedGenre = value; OnPropertyChanged(); }
        }

        private BookDTO _SelectedItem;
        public BookDTO SelectedItem
        {
            get { return _SelectedItem; }
            set { _SelectedItem = value; OnPropertyChanged(); }
        }

        #endregion

        #region ICommand
        public ICommand FirstLoadML { get; set; }
        public ICommand MaskNameML { get; set; }
        public ICommand SelectedGenreML { get; set; }
        public ICommand LoadBuyBookPageML { get; set; }
        public ICommand TurnOnBuyBook { get; set; }
        public ICommand LoadBookLocationPageML { get; set; }
        public ICommand TurnOnBookLocation { get; set; }
        public ICommand LoadSettingPageML { get; set; }
        public ICommand TurnOnSetting { get; set; }
        public ICommand LoadDetailBook { get; set; }
        public ICommand SignOutML { get; set; }
        public ICommand SortBookByMoney { get; set; }
        public ICommand TurnOnAscending { get; set; }
        public ICommand TurnOnDecreasing { get; set; }

        #endregion

        public static Grid MaskName { get; set; }
        public static CustomerDTO CurrentCustomer { get; set; }
        public bool isAscending { get; set; }

        public MainCustomerViewModel()
        {
            // Load ban đầu
            FirstLoadML = new RelayCommand<Frame>((p) => { return true; }, async (p) => 
            {
                ListBook1 = new ObservableCollection<BookDTO>(await BookServices.Ins.GetAllbook());
                GenreBook = new ObservableCollection<string>(baseBook.ListTheLoai);
                isAscending = true;
            });

            // Load trang mua sách
            LoadBuyBookPageML = new RelayCommand<Frame>((p) => { return true; }, async (p) => 
            {
                await LoadMainListBox(0);
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

            // Lọc thông tin theo thể loại
            SelectedGenreML = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                await LoadMainListBox(1);
            });

            // Load mặt nạ
            MaskNameML = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            // Mở window detail book
            LoadDetailBook = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DetailBook w;

                MaskName.Visibility = System.Windows.Visibility.Visible;

                DetailBookViewModel.selectBook = SelectedItem;

                w = new DetailBook();
                w.ShowDialog();
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

            // Sắp xếp sách theo tiền
            SortBookByMoney = new RelayCommand<MaterialDesignThemes.Wpf.PackIcon>((p) => { return true; }, async (p) =>
            {
                p.Visibility = Visibility.Collapsed;

                await SortBook(isAscending);

                if (isAscending) { isAscending = false; }
                else { isAscending = true; }
            });

            // Bật sắp xếp tăng dần
            TurnOnAscending = new RelayCommand<MaterialDesignThemes.Wpf.PackIcon>((p) => { return true; }, async (p) =>
            {
                p.Visibility = Visibility.Visible;
            });

            // Bật sắp xếp giảm dần
            TurnOnDecreasing = new RelayCommand<MaterialDesignThemes.Wpf.PackIcon>((p) => { return true; }, async (p) =>
            {
                p.Visibility = Visibility.Visible;
            });


        }

        
    }
}
