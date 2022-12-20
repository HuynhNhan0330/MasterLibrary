using MasterLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace MasterLibrary.Models.DataProvider
{
    public class InputBookServices
    {
        private static InputBookServices _ins;
        public static InputBookServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new InputBookServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        //get receipt
        public async Task<List<InputBookDTO>> GetBookInput()
        {
            List<InputBookDTO> BookInput;
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    BookInput = await (from bi in context.NHAPSACHes
                                       select new InputBookDTO
                                       {
                                           IDInput = bi.IDNHAP,
                                           IDBook = (int)bi.IDSACH,
                                           TenSach = bi.TENSACH,
                                           GiaNhap = (int)bi.GIANHAP,
                                           NgNhap = bi.NGNHAP,
                                           SoLuong = (int)bi.SOLUONG,
                                       }).ToListAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return BookInput;
        }


        //get receipt by month
        public async Task<List<InputBookDTO>> GetBookInput(int month)
        {
            List<InputBookDTO> BookInput;
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    BookInput = await (from bi in context.NHAPSACHes
                                       where bi.NGNHAP.Year == DateTime.Today.Year && bi.NGNHAP.Month == month
                                       select new InputBookDTO
                                       {
                                           IDInput = bi.IDNHAP,
                                           IDBook = (int)bi.IDSACH,
                                           TenSach = bi.TENSACH,
                                           GiaNhap = (int)bi.GIANHAP,
                                           NgNhap = bi.NGNHAP,
                                           SoLuong = (int)bi.SOLUONG,
                                       }).ToListAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return BookInput;
        }
    }
}
