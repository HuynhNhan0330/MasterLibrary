using System.Windows.Controls;
using System.Windows.Input;
using MasterLibrary.Views.Customer.BuyBookPage;
using MasterLibrary.Views.Customer.BookLocationPage;
using MasterLibrary.DTOs;
using System.Collections.ObjectModel;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Utils;

namespace MasterLibrary.ViewModel.CustomerVM
{
    public partial class MainCustomerViewModel: BaseViewModel
    {
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

        private string _SelectedItem;
        public string SelectedItem
        {
            get { return _SelectedItem; }
            set { _SelectedItem = value; OnPropertyChanged(); }
        }

        public static Grid MaskName { get; set; }
        public static CustomerDTO CurrentCustomer { get; set; }

        public ICommand FirstLoadML { get; set; }
        public ICommand LoadBuyBookPageML { get; set; }
        public ICommand LoadBookLocationPageML { get; set; }
        public ICommand SelectedGenreML { get; set; }
        public ICommand MaskNameML { get; set; }
        public ICommand LoadDetailBook { get; set; }

        public MainCustomerViewModel()
        {
            // Load ban đầu
            FirstLoadML = new RelayCommand<Frame>((p) => { return true; }, async (p) => 
            {
                ListBook1 = new ObservableCollection<BookDTO>(await BookServices.Ins.GetAllbook());
                GenreBook = new ObservableCollection<string>(baseBook.ListTheLoai);
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

            // Load thông tin theo thể loại
            SelectedGenreML = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                await LoadMainListBox(1);
            });

            MaskNameML = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            LoadDetailBook = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DetailBook w;

                MaskName.Visibility = System.Windows.Visibility.Visible;

                w = new DetailBook();
                w.ShowDialog();
            });
        }
    }
}
