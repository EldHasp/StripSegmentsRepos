using System;
using System.ComponentModel;

namespace Common
{
    #region Делегаты для методов WPF команд
    /// <summary>Делегат исполнительного метода команды без параметра</summary>
    public delegate void ExecuteActionHandler();
    /// <summary>Делегат исполнительного метода статуса команды без параметра</summary>
    /// <returns><see langword="true"/> если выполнение команды разрешено</returns>
    public delegate bool CanExecuteActionHandler();


    #endregion

    /// <summary>Класс для команд без параметров</summary>
    public class RelayActionCommand : RelayCommand
    {
        /// <summary>Конструктор команды</summary>
        /// <param name="executeAction">Выполняемый метод команды</param>
        /// <param name="canExecuteAction">Метод разрешающий выполнение команды</param>
        public RelayActionCommand(ExecuteActionHandler executeAction, CanExecuteActionHandler canExecuteAction = null)
        {
            execute = ExecuteAction;
            canExecute = CanExecuteAction;

            this.executeAction = executeAction ?? throw new ArgumentNullException(nameof(execute));
            this.canExecuteAction = canExecuteAction ?? CanExecuteActionTrue;
        }

        protected ExecuteActionHandler executeAction;
        protected CanExecuteActionHandler canExecuteAction;

        /// <summary>Метод всегда возвращающий <see langword="true"/>.
        /// Задан для уменьшения создания лямбд.</summary>
        /// <returns><see langword="true"/>.</returns>
        public static bool CanExecuteActionTrue() => true;

        /// <summary>Определяет метод, вызываемый при вызове данной команды.
        /// Задан чтобы не создавать лямбды.</summary>
        /// <param name="parameter">Неиcпользуется.</param>
        protected void ExecuteAction(object parameter) => executeAction();
        /// <summary>Определяет метод, который определяет, может ли данная команда
        /// выполняться в ее текущем состоянии.
        /// Задан чтобы не создавать лямбды.</summary>
        /// <param name="parameter">Неиспользуется.</param>
        protected bool CanExecuteAction(object parameter) => canExecuteAction();


        /// <summary>Определяет метод, вызываемый при вызове данной команды.</summary>
        public void ExecuteAction() => executeAction();

        /// <summary>Определяет метод, который определяет, может ли данная команда
        /// выполняться в ее текущем состоянии.</summary>
        /// <returns>Значение true, если эту команду можно выполнить;
        /// в противном случае — значение false.</returns>
        public bool CanExecuteAction() => canExecuteAction();
    }
}
