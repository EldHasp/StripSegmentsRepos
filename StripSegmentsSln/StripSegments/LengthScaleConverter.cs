using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace StripSegments
{
    public class LengthScaleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Все параметры в массиве values:
            // 0 - Преобразуемая Длина
            // 1 - длина Диапазона
            // 2 - ширина Контрола
            if (values == null)
                return null;

            // Получение первых трёх значений и перевод их в double.
            double[] nums = values.Take(3).OfType<double>().ToArray();
            if (nums.Length != 3)
                return null;

            // Присваивание значений переменным для наглядности
            double lengthSource = nums[0];
            double lengthRange = nums[1];
            double width = nums[2];

            return lengthSource * width / lengthRange;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
