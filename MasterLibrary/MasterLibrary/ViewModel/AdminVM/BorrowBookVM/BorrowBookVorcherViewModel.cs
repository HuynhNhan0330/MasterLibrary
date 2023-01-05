using MasterLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MasterLibrary.ViewModel.AdminVM.BorrowBookVM
{
    public partial class BorrowBookViewModel: BaseViewModel
    {
        #region Thuộc tính
        private ObservableCollection<BookInBorrowDTO> _ListBookInBorrow;
        public ObservableCollection<BookInBorrowDTO> ListBookInBorrow
        {
            get { return _ListBookInBorrow; }
            set { _ListBookInBorrow = value; OnPropertyChanged(); }
        }

        #endregion

        #region ICommand
        public ICommand FirstLoadBrrowBookVocher { get; set; }


        #endregion

    }
}
