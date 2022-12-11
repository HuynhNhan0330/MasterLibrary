using Haley.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MasterLibrary.Models.DataProvider
{
    public partial class StatisticServices
    {
        private static StatisticServices _ins;
        public static StatisticServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new StatisticServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        //Tính tiền thu theo năm
        public async Task<(List<decimal>, decimal)> GetRevenueByYear(int year)
        {
            decimal inputMoney = (decimal)0;
            List<decimal> revenueByMonthList = new List<decimal>(new decimal[12]);

            using (var context = new MasterlibraryEntities())
            {
                var billList = context.HOADONs.Where(b => b.NGHD.Year == year);

                if (billList.ToList().Count != 0)
                {
                    inputMoney = (decimal) billList.Sum(b => b.TRIGIA);
                }
                

                var revenueByMonth = billList.GroupBy(b => b.NGHD.Month).Select(gr => new { Month = gr.Key, Income = gr.Sum(b => (decimal?)b.TRIGIA) ?? 0 }).ToList();
            
                foreach (var re in revenueByMonth)
                {
                    revenueByMonthList[re.Month - 1] = decimal.Truncate(re.Income);
                }
                return (revenueByMonthList, inputMoney);
            }
        }

        //Tính tiền chi theo năm
        public async Task<(List<decimal>, decimal)> GetExpenseByYear(int year)
        {
            decimal outputMoney = 0;
            List<decimal> expenseByMonthList = new List<decimal>(new decimal[12]);

            using (var context = new MasterlibraryEntities())
            {
                var receiptList = context.NHAPSACHes.Where(b => b.NGNHAP.Year == year);

                if (receiptList != null)
                {
                    outputMoney = (decimal)receiptList.Sum(b => b.SOLUONG * b.GIANHAP);
                }
               
                var receiptByMonth = receiptList.GroupBy(b => b.NGNHAP.Month).Select(gr => new { Month = gr.Key, Output = gr.Sum(b => (decimal?)(b.SOLUONG * b.GIANHAP) ?? 0)}).ToList();

                foreach (var re in receiptByMonth)
                {
                    expenseByMonthList[re.Month - 1] = decimal.Truncate(re.Output);
                }

                return (expenseByMonthList, outputMoney);
            }
        }

        //tính tiền thu theo tháng

    }
}
