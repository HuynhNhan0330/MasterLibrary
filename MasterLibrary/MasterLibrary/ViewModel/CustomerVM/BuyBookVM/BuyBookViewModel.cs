using MasterLibrary.DTOs;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MasterLibrary.Models.DataProvider;
using System;
using System.Windows;
using System.Linq;

namespace MasterLibrary.ViewModel.CustomerVM
{
    public partial class MainCustomerViewModel : BaseViewModel
    {
        public async Task LoadMainListBox(int func)
        {
            if (ListBook != null)
            {
                ListBook.Clear();
            }

            switch (func)
            {
                case 0:
                    // Load hết sách
                    try
                    {
                        ListBook = new ObservableCollection<BookDTO>(await Task.Run(() => BookServices.Ins.GetAllbook()));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Lỗi", "Mất kết nối cơ sở dữ liệu");
                    }
                    break;
                case 1:
                    await FilterBookByGenre();
                    break;
            }
        }

        public async Task FilterBookByGenre()
        {
            await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(SelectedGenre))
                {
                    ListBook = new ObservableCollection<BookDTO>(ListBook1);
                }
                else
                {
                    // Load sách theo thể loại
                    ObservableCollection<BookDTO> tmpListBook = new ObservableCollection<BookDTO>();

                    foreach (var item in ListBook1)
                    {
                        if (item.TheLoai == SelectedGenre)
                        {
                            tmpListBook.Add(item);
                        }
                    }

                    ListBook = new ObservableCollection<BookDTO>(tmpListBook);
                }
            });
        }

        public async Task SortBook(bool ascending)
        {
            await Task.Run(() =>
            {
                ObservableCollection<BookDTO> tmpListBook = new ObservableCollection<BookDTO>(ListBook);

                if (ascending) ListBook = new ObservableCollection<BookDTO>(tmpListBook.OrderBy(a => a.Gia));
                else ListBook = new ObservableCollection<BookDTO>(tmpListBook.OrderByDescending(a => a.Gia));
            });
        }
    }
}
