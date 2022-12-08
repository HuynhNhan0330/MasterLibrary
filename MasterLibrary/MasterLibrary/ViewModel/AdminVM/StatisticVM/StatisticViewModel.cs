using Avalonia.Controls;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.ViewModel.AdminVM.StatisticVM
{
    public class StatisticViewModel : BaseViewModel
    {
        private SeriesCollection _IncomdeData;
        public SeriesCollection IncomdeData
        {
            get { return _IncomdeData};
            set { _IncomdeData = value; OnPropertyChanged(); }
        }

        private ComboBoxItem _SelectedPeriod;
        public ComboBoxItem SelectedPeriod
        {
            get { return _SelectedPeriod; }
            set { _SelectedPeriod = value; OnPropertyChanged(); }
        }

        private string _SelectecTime;
        public string SelectecTime
        {
            get { return _SelectecTime;}
            set { _SelectecTime = value; OnPropertyChanged(); }
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

        public async Task ChangePeriod()
        {

        }
    }
}
