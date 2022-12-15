using MasterLibrary.DTOs;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using MasterLibrary.Views.Admin.LocationPage;

namespace MasterLibrary.ViewModel.AdminVM.LocationVM
{
    public class DetailBookInRowViewModel: BaseViewModel
    {
        #region Thuộc tính
        private BookDTO _BookCurrent;
        public BookDTO BookCurrent
        {
            get { return _BookCurrent; }
            set
            {
                _BookCurrent = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region ICommand
        public ICommand FirstLoadDetailBookInRow { get; set; }
        public ICommand CloseDetailBookInRow { get; set; }

        #endregion

        #region thuộc tính tạm thời
        public static BookDTO selectBook { get; set; }

        #endregion

        public DetailBookInRowViewModel()
        {
            // Load lần đầu
            FirstLoadDetailBookInRow = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                BookCurrent = selectBook;
            });

            // Đóng trang detailBook
            CloseDetailBookInRow = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DetailBookInRow w = Application.Current.Windows.OfType<DetailBookInRow>().FirstOrDefault();
                w.Close();
            });
        }
    }
}
