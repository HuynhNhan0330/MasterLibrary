using MasterLibrary.DTOs;
using MasterLibrary.ViewModel.CustomerVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

                        if (_sach.SL < BookInBorrowList[i].SoLuong)
                        {
                            return (false, BookInBorrowList[i].TenSach + "vượt số lượng cho phép vui lòng làm mới trang hoặc chỉnh lại số lượng cho phép");
                        }

                        _sach.SL -= BookInBorrowList[i].SoLuong;
                    }

                    context.SaveChanges();

                    return (true, "Thuê thành công");
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return (false, "Xãy ra lỗi khi thao tác dữ liệu trên cơ sở dữ liệu");
            }
            catch (Exception)
            {
                return (false, "Xãy ra lỗi khi thực hiện thao tác");
            }
        }

        public async Task<List<BookInBorrowDTO>> GetBookBorrowCustomer(int _makh)
        {
            List<BookInBorrowDTO> bookborrows = null;

            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    bookborrows = await (from sachmuon in context.PHIEUMUONs
                                   join sach in context.SACHes on sachmuon.MASACH equals sach.MASACH
                                   where sachmuon.MAKH == _makh
                                   select new BookInBorrowDTO
                                   {
                                       MaPhieuMuon = sachmuon.MAPHIEUMUON,
                                       MaSach = (int)sachmuon.MASACH,
                                       TenSach = sach.TENSACH,
                                       img = sach.IMAGESOURCE,
                                       NgayHetHan = (DateTime)sachmuon.NGAYHETHAN,
                                       SoLuong = (int)sachmuon.SOLUONG,
                                       Gia = (int)sach.GIA
                                   }
                     ).ToListAsync();
                }
            }
            catch (Exception)
            {

            }

            return bookborrows;
        }

        public async Task<(bool, string)> CreateNewReceipt(int idCustomer, ObservableCollection<BookInCollectDTO> BookInCollectList)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    for (int i = 0; i < BookInCollectList.Count; ++i)
                    {
                        PHIEUTHU newPhieuMuon = new PHIEUTHU();
                        newPhieuMuon.MAKH = idCustomer;
                        newPhieuMuon.MASACH = BookInCollectList[i].MaSach;
                        newPhieuMuon.NGAYTHU = BookInCollectList[i].NgayTra;
                        newPhieuMuon.SOLUONG = BookInCollectList[i].SoLuong;
                        newPhieuMuon.TIENPHATHONG = BookInCollectList[i].TienHong;
                        newPhieuMuon.SOLUONGHONG = BookInCollectList[i].SoLuongHong;
                        newPhieuMuon.TIENTREMOTNGAY = BookInCollectList[i].TienTre;
                        newPhieuMuon.TONGTIEN = BookInCollectList[i].TongTienTra;

                        // cộng lại số lượng sách đã thuê

                        var _phieumuon = await context.PHIEUMUONs.FindAsync(BookInCollectList[i].MaPhieuMuon);
                        
                        if (_phieumuon != null)
                        {
                            _phieumuon.SOLUONG -= BookInCollectList[i].SoLuong;

                            if (_phieumuon.SOLUONG == 0)
                            {
                                context.PHIEUMUONs.Remove(_phieumuon);
                            }
                        }

                        var _sach = await context.SACHes.FindAsync(BookInCollectList[i].MaSach);
                        if (_sach != null) _sach.SL += BookInCollectList[i].SoLuong;
                    }

                    context.SaveChanges();

                    return (true, "Thu thành công");
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return (false, "Xãy ra lỗi khi thao tác dữ liệu trên cơ sở dữ liệu");
            }
            catch (Exception)
            {
                return (false, "Xãy ra lỗi khi thực hiện thao tác");
            }
        }

    }
}
