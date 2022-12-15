using MasterLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.Models.DataProvider
{
    public class BillServices
    {
        private static BillServices _ins;
        public static BillServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BillServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<List<BillDTO>> GetAllBill()
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    var BillList = (from hoadon in context.HOADONs
                                    select new BillDTO
                                    {
                                        MAKH = (int)hoadon.MAKH,
                                        cusId = (int)hoadon.MAKH,
                                        cusName = hoadon.KHACHHANG.USERNAME,
                                        MAHD = hoadon.MAHD,
                                        TRIGIA = (decimal)hoadon.TRIGIA,
                                        NGHD = hoadon.NGHD
                                    }).ToListAsync();
                    return await BillList;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<BillDTO>> GetBillByMonth(int month)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    var BillList = (from hoadon in context.HOADONs
                                    where hoadon.NGHD.Year == DateTime.Now.Year && hoadon.NGHD.Month == month
                                    orderby hoadon.NGHD descending
                                    select new BillDTO
                                    {
                                        MAKH = (int)hoadon.MAKH,
                                        cusId = (int)hoadon.MAKH,
                                        cusName = hoadon.KHACHHANG.USERNAME,
                                        MAHD = hoadon.MAHD,
                                        TRIGIA = (decimal)hoadon.TRIGIA,
                                        NGHD = hoadon.NGHD
                                    }).ToListAsync();
                    return await BillList;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<BillDTO>> GetBillByDate(DateTime date)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    var BillList = (from hoadon in context.HOADONs
                                    where DbFunctions.TruncateTime(hoadon.NGHD) == date.Date
                                    select new BillDTO
                                    {
                                        MAKH = (int)hoadon.MAKH,
                                        cusId = (int)hoadon.MAKH,
                                        cusName = hoadon.KHACHHANG.USERNAME,
                                        MAHD = hoadon.MAHD,
                                        TRIGIA = (decimal)hoadon.TRIGIA,
                                        NGHD = hoadon.NGHD
                                    }).ToListAsync();
                    return await BillList;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
