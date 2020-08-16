using System;
using System.Globalization;
using System.Windows.Data;

namespace StripSegments
{
    /// <summary>Мультиконвертер для расчёта Maximum и Minimum ScrollBar.</summary>
    public class SizeToValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)parameter == "+")
                return (double)values[0] + (double)values[1] * 0.5;
            if ((string)parameter == "-")
                return (double)values[0] - (double)values[1] * 0.5;
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
