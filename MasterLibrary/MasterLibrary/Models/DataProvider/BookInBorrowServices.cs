using MasterLibrary.DTOs;
using MasterLibrary.ViewModel.CustomerVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.Models.DataProvider
{
    public class BookInBorrowServices
    {
        private static BookInBorrowServices _ins;
        public static BookInBorrowServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BookInBorrowServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<(bool, string)> CreateNewCallCard(int idCustomer, DateTime _ExpirationDate, ObservableCollection<BookInBorrowDTO> BookInBorrowList)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    for (int i = 0; i < BookInBorrowList.Count; ++i)
                    {
                        PHIEUMUON newPhieuMuon = new PHIEUMUON();
                        newPhieuMuon.MAKH = idCustomer;
                        newPhieuMuon.MASACH = BookInBorrowList[i].MaSach;
                        newPhieuMuon.NGAYHETHAN = _ExpirationDate;
                        newPhieuMuon.SOLUONG = BookInBorrowList[i].SoLuong;

                        context.PHIEUMUONs.Add(newPhieuMuon);

                        // Trừ đi số lượng sách đã thuê

                        var _sach = await context.SACHes.FindAsync(BookInBorrowList[i].MaSach);
                        _sach.SL -= BookInBorrowList[i].SoLuong;
                    }

                    context.SaveChanges();

                    return (true, "Thuê thành công");
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return (false, "Xãy ra lỗi khi thêm dữ liệu vào cơ sở dữ liệu");
            }
            catch (Exception)
            {
                return (false, "Xãy ra lỗi khi thực hiện thao tác");
            }
        }
    }
}
