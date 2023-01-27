using System;
using System.Globalization;
using System.Windows.Data;

namespace MasterLibrary.Utils.ConverterValue
{
    public class ForegroundStatusBookInBorrowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime _NgayHetHan = (DateTime) value;

            if ((DateTime.Now - _NgayHetHan).Days > 0)
            {
                return "#ba1111";
            }
            else
            {
                return "#428720";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
