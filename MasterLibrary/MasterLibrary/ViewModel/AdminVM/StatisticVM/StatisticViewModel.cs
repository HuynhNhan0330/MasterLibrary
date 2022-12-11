﻿using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MasterLibrary.Views.MessageBoxML;

namespace MasterLibrary.ViewModel.AdminVM.StatisticVM
{
    public class StatisticViewModel : BaseViewModel
    {

        public ICommand ChangePeriodML { get; set; }

        #region variable
        private SeriesCollection _IncomeData;
        public SeriesCollection IncomeData
        {
            get { return _IncomeData; }
            set { _IncomeData = value; OnPropertyChanged(); }
        }

        private ComboBoxItem _SelectedPeriod;
        public ComboBoxItem SelectedPeriod
        {
            get { return _SelectedPeriod; }
            set { _SelectedPeriod = value; OnPropertyChanged(); }
        }

        private string _SelectedTime;
        public string SelectedTime
        {
            get { return _SelectedTime;}
            set { _SelectedTime = value; OnPropertyChanged(); }
        }

        private int _SelectedYear;
        public int SelectedYear
        {
            get { return _SelectedYear; }
            set { _SelectedYear = value; }
        }

        private string _TrueIncome;
        public string TrueIncome
        {
            get { return _TrueIncome; }
            set { _TrueIncome = value; OnPropertyChanged(); }
        }

        private string _TotalIn;
        public string TotalIn
        {
            get { return _TotalIn; }
            set { _TotalIn = value; OnPropertyChanged(); }
        }

        private string _TotalOut;
        public string TotalOut
        {
            get { return _TotalOut; }
            set { _TotalOut = value; OnPropertyChanged(); }
        }

        private int _LabelMaxValue;
        public int LabelMaxValue
        {
            get { return _LabelMaxValue; }
            set { _LabelMaxValue = value; OnPropertyChanged(); }
        }
        #endregion
        public StatisticViewModel()
        {
            ChangePeriodML = new RelayCommand<ComboBox>((p) => { return true; }, async(p) =>
            {
                if (SelectedPeriod != null)
                {
                    switch (SelectedPeriod.Content.ToString())
                    {
                        case "Theo năm":
                            {
                                if (SelectedTime != null)
                                {
                                    if (SelectedTime.Length == 4)
                                        SelectedYear = int.Parse(SelectedTime);
                                    await LoadIncomeByYear();
                                }
                                return;
                            }
                        case "Theo tháng":
                            {
                                if (SelectedTime != null)
                                {
                                    LoadIncomeByMonth();
                                }
                                return;
                            }
                    }
                }
            }
            );
        }

        public async Task LoadIncomeByYear()
        {
            if (SelectedTime.Length != 4) return;
            LabelMaxValue = 12;
            List<decimal> RevenueByMonthList = new List<decimal>();
            try
            {
                (List<decimal> MonthlyRevenue, decimal totalin) = await Task.Run(() => StatisticServices.Ins.GetRevenueByYear(int.Parse(SelectedTime)));
                TotalIn = Helper.FormatVNMoney(totalin);
                MonthlyRevenue.Insert(0, 0);
                for (int i = 1; i <= 12; i++)
                {
                    MonthlyRevenue[i] /= 1000000;
                }

                IncomeData = new SeriesCollection
            {   
                new LineSeries
                {
                    Title = "Thu",
                    Values = new ChartValues<decimal>(MonthlyRevenue),
                    Fill = Brushes.Transparent,
                },
                new LineSeries
                {
                    Title = "Chi",
                    
                }
            };
            }
            catch (System.Data.Entity.Core.EntityException e)
            {
                Console.WriteLine(e);
                MessageBoxML mb = new MessageBoxML("Lỗi", "Mất kết nối cơ sở dữ liệu", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBoxML mb = new MessageBoxML("Lỗi", "Lỗi hệ thống", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
                throw;
            }

    //(List<decimal> MonthlyRevenue, decimal totalin) = await Task.Run(() => StatisticServices.Ins.GetRevenueByYear(int.Parse(SelectedTime)));
    //        TotalIn = Helper.FormatVNMoney(totalin);

            
        }

        public void LoadIncomeByMonth()
        {

        }
    }
}
