using System;
using System.ComponentModel;

namespace Common
{
    #region Делегаты для методов WPF команд
    /// <summary>Делегат исполнительного метода команды</summary>
    /// <param name="parameter">Параметр команды</param>
    public delegate void ExecuteHandler<T>(T parameter);
    /// <summary>Делегат исполнительного метода статуса команды</summary>
    /// <param name="parameter">Параметр команды</param>
    /// <returns><see langword="true"/> если выполнение команды разрешено</returns>
    public delegate bool CanExecuteHandler<T>(T parameter);
    #endregion

    /// <summary>Класс для команд без параметров</summary>
    public class RelayCommand<T> : RelayCommand
    {

        /// <summary>Конструктор команды</summary>
        /// <param name="executeT">Выполняемый метод команды</param>
        /// <param name="canExecuteT">Метод разрешающий выполнение команды</param>
        public RelayCommand(ExecuteHandler<T> executeT, CanExecuteHandler<T> canExecuteT = null)
        {
            executeT = ExecuteT;
            canExecuteT = CanExecuteT;

            this.executeT = this.executeT ?? throw new ArgumentNullException(nameof(executeT));
            this.canExecuteT = this.canExecuteT ?? CanExecuteTTrue;
        }

        protected ExecuteHandler<T> executeT;
        protected CanExecuteHandler<T> canExecuteT;

        /// <summary>Метод всегда возвращающий <see langword="true"/>.
        /// Задан для уменьшения создания лямбд.</summary>
        /// <param name="parameter">Не используется.</param>
        /// <returns><see langword="true"/>.</returns>
        public static bool CanExecuteTTrue(T parameter) => true;

        /// <summary>Определяет метод, вызываемый при вызове данной команды.
        /// Задан чтобы не создавать лямбды.</summary>
        /// <param name="parameter">>Данные, используемые данной командой.
        /// Если нельзя преобразовать в тип T,
        /// то команда не выполняется.</param>
        protected void ExecuteT(object parameter)
        {
            if (parameter is T t)
                executeT(t);
        }

        /// <summary>Определяет метод, который определяет, может ли данная команда
        /// выполняться в ее текущем состоянии.
        /// Задан чтобы не создавать лямбды.</summary>
        /// <param name="parameter">>Данные, используемые данной командой.
        /// Если нельзя преобразовать в тип T,
        /// то метод возвращает <see langword="false"/>.</param>
        /// <returns>Значение true, если эту команду можно выполнить;
        /// в противном случае — значение false.</returns>
        protected bool CanExecuteT(object parameter)
        {
            return parameter is T t && canExecuteT(t);
        }


        /// <summary>Определяет метод, вызываемый при вызове данной команды.</summary>
        /// <param name="parameter">Несипользуется.</param>
        public void ExecuteT(T parameter) => executeT(parameter);

        /// <summary>Определяет метод, который определяет, может ли данная команда
        /// выполняться в ее текущем состоянии.</summary>
        /// <param name="parameter">Данные, используемые данной командой.
        /// Если <see langword="null"/> - метод возвращает <see langword="false"/>.</param>
        /// <returns>Значение true, если эту команду можно выполнить;
        /// в противном случае — значение false.</returns>
        public bool CanExecuteT(T parameter) => canExecuteT(parameter);

    }
}
