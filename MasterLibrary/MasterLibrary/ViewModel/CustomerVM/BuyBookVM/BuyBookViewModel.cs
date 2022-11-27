using System.Windows.Controls;
using System.Windows.Input;
using MasterLibrary.DTOs;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MasterLibrary.Models.DataProvider;
using System;
using System.Windows;

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
    }
}
