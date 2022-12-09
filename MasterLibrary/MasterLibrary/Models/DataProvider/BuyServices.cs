using MasterLibrary.DTOs;
using MasterLibrary.Views.MessageBoxML;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace MasterLibrary.Models.DataProvider
{
    public class BuyServices
    {
        private static BuyServices _ins;
        public static BuyServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BuyServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<int> CreateNewBill(BillDTO bill)
        {
            using (var context = new MasterlibraryEntities())
            {
                int newIdBill = await context.HOADONs.MaxAsync(hd => hd.MAHD) + 1;

                HOADON newBill = new HOADON();
                newBill.NGHD = bill.NGHD;
                newBill.MAKH = bill.MaKH;
                newBill.TRIGIA = bill.TriGia;

                context.HOADONs.Add(newBill);

                context.SaveChanges();

                return newIdBill;
            }
        }

        public async Task CreateNewBillDetail(int IdBill, List<BillDetailDTO> BillDetailList)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    List<CTHD> newBillDetailList = new List<CTHD>();

                    for (int i = 0; i < BillDetailList.Count; ++i)
                    {
                        CTHD newCTHD = new CTHD();
                        newCTHD.MAHD = IdBill;
                        newCTHD.MASACH = BillDetailList[i].MaSach;
                        newCTHD.SOLUONG = BillDetailList[i].SoLuong;

                        newBillDetailList.Add(newCTHD);
                    }

                    context.CTHDs.AddRange(newBillDetailList);

                    context.SaveChanges();
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                MessageBoxML ms = new MessageBoxML("Lỗi", "Xãy ra lỗi khi thêm dữ liệu chi tiết hoá đơn vào database", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
            }
            catch (Exception)
            {
                MessageBoxML ms = new MessageBoxML("Lỗi", "Xãy ra lỗi khi xử lí dữ liệu", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
            }
        }

        

        public async Task CreateFullBill(BillDTO bill, List<BillDetailDTO> BillDetailList)
        {
            try
            {
                
                {
                    // Tạo hoá đơn mới
                    int billId = await CreateNewBill(bill);

                    //Tạo các chi tiết hoá đơn

                    await CreateNewBillDetail(billId, BillDetailList);

                    MessageBoxML ms = new MessageBoxML("Thông báo", "Mua thành công", MessageType.Accept, MessageButtons.OK);
                    ms.ShowDialog();
                }

            }
            catch (Exception)
            {
                MessageBoxML ms = new MessageBoxML("Lỗi", "Mua thất bại ", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
            }
        }
    }
}
