using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace Common
{
    /// <summary>Мультиконвертер - возвращает среднее double значение массива полученных значений.</summary>
    public class DoubleMediumConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return null;

            int count = 0;
            double sum = 0;
            foreach (object value in values)
            {
                if (value is double val)
                {
                    count++;

                    sum += val;
                }
                else if (TypeDescriptor.GetConverter(typeof(double)).IsValid(value))
                {
                    count++;

                    sum += (double)TypeDescriptor.GetConverter(typeof(double)).ConvertFrom(value);
                }
            }
            if (count == 0)
                return null;
            return sum / count;
        }



        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
