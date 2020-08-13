using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace StripSegments
{
    public class OffsetScaleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Все параметры в массиве values:
            // 0 - Начало Сегмента
            // 1 - Смещение (Начало) Диапазона
            // 2 - длина Диапазона
            // 3 - ширина Контрола
            if (values == null)
                return null;

            // Получение первых трёх значений и перевод их в double.
            double[] nums = values.Take(4).OfType<double>().ToArray();
            if (nums.Length != 4)
                return null;

            // Присваивание значений переменным для наглядности
            double begin = nums[0];
            double offset = nums[1];
            double length = nums[2];
            double width = nums[3];

            return (begin - offset) * width / length;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
