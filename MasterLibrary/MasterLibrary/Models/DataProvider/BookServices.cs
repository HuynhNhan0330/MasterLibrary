using MasterLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.Models.DataProvider
{
    public class BookServices
    {
        private static BookServices _ins;
        public static BookServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BookServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<List<BookDTO>> GetAllbook()
        {
            List<BookDTO> books = null;

            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    books = await (from sach in context.SACHes
                                    select new BookDTO
                                    {
                                        MaSach = sach.MASACH,
                                        TenSach = sach.TENSACH,
                                        TacGia = sach.TACGIA,
                                        MoTa = sach.MOTA,
                                        NXB = sach.NXB,
                                        NamXB = (int)sach.NAMXB,
                                        TheLoai = sach.THELOAI,
                                        Gia = (decimal)sach.GIA,
                                        SoLuong = (int)sach.SL,
                                        ImageSource = sach.IMAGESOURCE,
                                        ViTriTang = (int)sach.VITRITANG,
                                        ViTriDay = (int)sach.VITRIDAY
                                    }
                     ).ToListAsync();
                }
            }
            catch (Exception)
            {

            }

            return books;
        }
    }
}
