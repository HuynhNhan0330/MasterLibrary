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

                if (receiptList.ToList().Count != 0)
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
        public async Task<(List<decimal>, decimal)> GetRevenueByMonth(int year, int month)
        {
            decimal inputMoney = (decimal)0;
            int days = DateTime.DaysInMonth(year, month);
            List<decimal> revenueByDayList = new List<decimal>(new decimal[days]);

            using (var context = new MasterlibraryEntities())
            {
                var billList = context.HOADONs.Where(b => b.NGHD.Year == year && b.NGHD.Month == month);

                if (billList.ToList().Count != 0)
                {
                    inputMoney = (decimal)billList.Sum(b => b.TRIGIA);
                }

                var revenueByDay = billList.GroupBy(b => b.NGHD.Day).Select(gr => new { Day = gr.Key, Income = gr.Sum(b => (decimal?)b.TRIGIA) ?? 0 }).ToList();

                foreach (var re in revenueByDay)
                {
                    revenueByDayList[re.Day - 1] = decimal.Truncate(re.Income);
                }

                return (revenueByDayList, inputMoney);
            }
        }

        public async Task<(List<decimal>, decimal)> GetExpenseByMonth(int year, int month)
        {
            decimal outputMoney = (decimal)0;
            int days = DateTime.DaysInMonth(year, month);
            List<decimal> expenseByDayList = new List<decimal>(new decimal[days]);

            using (var context = new MasterlibraryEntities())
            {
                var receiptList = context.NHAPSACHes.Where(b => b.NGNHAP.Year == year && b.NGNHAP.Month == month);

                if (receiptList.ToList().Count != 0)
                {
                    outputMoney = (decimal)receiptList.Sum(b => (b.SOLUONG * b.GIANHAP));
                }

                var revenueByDay = receiptList.GroupBy(b => b.NGNHAP.Day).Select(gr => new { Day = gr.Key, Income = gr.Sum(b => (decimal?)(b.SOLUONG * b.GIANHAP) ?? 0 )}).ToList();

                foreach (var re in revenueByDay)
                {
                    expenseByDayList[re.Day - 1] = decimal.Truncate(re.Income);
                }

                return (expenseByDayList, outputMoney);
            }
        }
    }
}
