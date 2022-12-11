using Haley.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        //public int calTongThuByYear(int year)
        //{
        //    int inputList = 0;
        //    var billList = db.HOADONs.Where(b => b.NGHD.Year == year);
        //    foreach (var bill in billList)
        //    {
        //        inputList = (int)db.HOADONs.Sum(b => b.TRIGIA);
        //    }

        //    //var revenueByMonth = billList.GroupBy(b => b.NGHD.Month).Select(gr => new { Month = gr.Key, Income = gr.Sum(b => (decimal?)b.TRIGIA) ?? 0 }).ToList();
        //    //foreach (var re in revenueByMonth)
        //    //{
        //    //    revenueByMonthList[re.Month - 1] = decimal.Truncate(re.Income);
        //    //}, ref List<decimal> revenueByMonthList
        //    return (inputList);
        //}

        public async Task<(List<decimal>, decimal)> GetRevenueByYear(int year)
        {
            decimal inputList = 0;
            List<decimal> revenueByMonthList = new List<decimal>(new decimal[12]);

            using (var context = new MasterlibraryEntities())
            {
                var billList = context.HOADONs.Where(b => b.NGHD.Year == year);
                
                foreach (var bill in billList)
                {
                    inputList = (decimal)context.HOADONs.Sum(b => b.TRIGIA);
                }

                var revenueByMonth = billList.GroupBy(b => b.NGHD.Month).Select(gr => new { Month = gr.Key, Income = gr.Sum(b => (decimal?)b.TRIGIA) ?? 0 }).ToList();
            
                foreach (var re in revenueByMonth)
                {
                    revenueByMonthList[re.Month - 1] = decimal.Truncate(re.Income);
                }
                //revenueByMonthList[0] = revenueByMonthList[1];
                return (revenueByMonthList, inputList);
            }
            
            //var billList = db.HOADONs.Where(b => b.NGHD.Year == year);
            //foreach (var bill in billList)
            //{
            //    inputList = (decimal)db.HOADONs.Sum(b => b.TRIGIA);
            //}

            //var revenueByMonth = billList.GroupBy(b => b.NGHD.Month).Select(gr => new { Month = gr.Key, Income = gr.Sum(b => (decimal?)b.TRIGIA) ?? 0 }).ToList();
            //foreach (var re in revenueByMonth)
            //{
            //    revenueByMonthList[re.Month - 1] = decimal.Truncate(re.Income);
            //}
        }

        //public async Task<(List<decimal>, decimal)> GetExpenseByYear(int year)
        //{
        //    decimal outputList = 0;
        //    List<decimal> expenseByYearList = new List<decimal>(new decimal[12]);

        //    using (var context = new MasterlibraryEntities())
        //    {
        //        var receiptList = context.NHAPSACHes.Where(b => b.NGNHAP)
        //    }
        //}
    }
}
