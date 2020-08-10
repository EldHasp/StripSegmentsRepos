using System;
using System.Windows;
using System.Windows.Input;

namespace Common
{
    public interface ICommandInvalidate : ICommand
    {
        /// <summary>Метод вызываемый для создания события CanExecuteChanged</summary>
        void Invalidate();
    }

    #region Делегаты для методов WPF команд
    /// <summary>Делегат исполнительного метода команды</summary>
    /// <param name="parameter">Параметр команды</param>
    public delegate void ExecuteHandler(object parameter);
    /// <summary>Делегат исполнительного метода статуса команды</summary>
    /// <param name="parameter">Параметр команды</param>
    /// <returns><see langword="true"/> если выполнение команды разрешено</returns>
    public delegate bool CanExecuteHandler(object parameter);
    #endregion

    #region Класс команд - RelayCommand
    /// <summary>Класс реализующий интерфейс ICommand для создания WPF команд</summary>
    public class RelayCommand : ICommandInvalidate
    {
        protected CanExecuteHandler canExecute;
        protected ExecuteHandler execute;
        private readonly EventHandler requerySuggested;
        /// <summary>Метод всегда возвращающий <see langword="true"/>.
        /// Задан для уменьшения создания лямбд.</summary>
        /// <param name="parameter">Не используется.</param>
        /// <returns><see langword="true"/>.</returns>
        public static bool CanExecuteTrue(object parameter) => true;

        /// <summary>Событие извещающее об измении состояния команды</summary>
        public event EventHandler CanExecuteChanged;

        protected RelayCommand()
        {
            requerySuggested = (o, e) => Invalidate();
            CommandManager.RequerySuggested += requerySuggested;
        }

        /// <summary>Конструктор команды</summary>
        /// <param name="execute">Выполняемый метод команды</param>
        /// <param name="canExecute">Метод разрешающий выполнение команды</param>
        public RelayCommand(ExecuteHandler execute, CanExecuteHandler canExecute = null)
            : this()
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

            this.canExecute = canExecute ?? CanExecuteTrue;
        }

        /// <summary>Метод создания события CanExecuteChanged.</summary>
        private void RiseCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public void Invalidate()
        {
            if (CanExecuteChanged != null)
                Application.Current.Dispatcher.BeginInvoke((Action)RiseCanExecuteChanged);
        }

        public bool CanExecute(object parameter) => canExecute(parameter);

        public void Execute(object parameter) => execute.Invoke(parameter);

    }

    #endregion

}
