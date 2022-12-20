using MasterLibrary.DTOs;
using System;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using System.Data.Entity;
>>>>>>> nam/history
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
<<<<<<< HEAD
=======

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

        //public async Task<BillDTO> GetDetail(int ID)
        //{
        //    try
        //    {
        //        using (var context = new MasterlibraryEntities())
        //        {
        //            var bill = await context.HOADONs.FindAsync(ID);
        //            var cthd = bill.CTHDs;
        //            //MessageBox.Show(cthd.First().MASACH.ToString());
        //            //var IDBook = cthd.FirstOrDefault().MASACH;
        //            //var namebook = context.SACHes.Where(p => p.MASACH == IDBook).Take(1).ToString();
        //            BillDTO billInfo = new BillDTO
        //            {
        //                MAHD = bill.MAHD,
        //                MAKH = (int)bill.MAKH,
        //                NGHD = bill.NGHD,
        //                TRIGIA = (decimal)bill.TRIGIA,
        //                cusName = bill.KHACHHANG.TENKH,
        //                cusAdd = bill.KHACHHANG.DIACHI,
        //                //bookName = namebook,
        //            };

        //            return billInfo;
        //        }
        //    }
        //    catch (Exception e) 
        //    {
        //        throw e;
        //    }
        //}
>>>>>>> nam/history
    }
}
