using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace Common
{
    /// <summary>Конвертер проверяющий состояние команды</summary>
    /// <remarks>Mожет использоваться как мультиконверер с передачей параметра или без, 
    /// и как простой конвертер с привязкой только к команде</remarks>
    public class IsCanExecuteCommandConverter : IMultiValueConverter, IValueConverter
    {
        /// <summary>Возвращает состояние команды</summary>
        /// <param name="values">В первом элементе - команда, во втором - параметр команды. 
        /// Если нет второго параметра, то проверяется для <see langword="null"/></param>
        /// <param name="targetType">Не используется</param>
        /// <param name="parameter">Не используется</param>
        /// <param name="culture">Не используется</param>
        /// <returns>Состояние командя для указанног параметра</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 1 || !(values[0] is ICommand command))
                return false;

            if (values.Length < 2)
                return command.CanExecute(null);
            return command.CanExecute(values[1]);
        }

        /// <summary>Возвращает состояние команды</summary>
        /// <param name="value">Команда</param>
        /// <param name="targetType">Не используется</param>
        /// <param name="parameter">Не используется</param>
        /// <param name="culture">Не используется</param>
        /// <returns>Состояние командя для параметра = <see langword="null"/></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is ICommand command))
                return false;
            return command.CanExecute(null);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
