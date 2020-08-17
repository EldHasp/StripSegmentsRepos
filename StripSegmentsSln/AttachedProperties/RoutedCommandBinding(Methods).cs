using System;
using System.Windows;
using System.Windows.Input;

namespace AttachedProperties
{
	public partial class RoutedCommandBinding 
	{
		#region Methods Declaration

		/// <summary>Default CanExecute Method.</summary>
		/// <param name="parameter">Command Parameter.</param>
		/// <returns>Always <see langword="true"/>.</returns>
		public static bool CanExecuteDefault(object parameter) => true;

		/// <summary>Default Execute Method.</summary>
		/// <param name="parameter">Command Parameter.</param>
		/// <remarks>Empty body.</remarks>
		public static void ExecuteDefault(object parameter) { }

		/// <summary>Delegate for CanExecute.</summary>
		protected Func<object, bool> canExecuteDelegate = CanExecuteDefault;

		/// <summary>Delegate for Execute.</summary>
		protected Action<object> executeDelegate = ExecuteDefault;

		/// <summary>Method for CommandBinding.CanExecuteRouted.</summary>
		/// <param name="sender">The command target that is invoking the handler.</param>
		/// <param name="e">The event data.</param>
		protected virtual void CanExecuteRoutedMethod(object sender, CanExecuteRoutedEventArgs e)
		{
			e.Handled = Handled;
			e.CanExecute = canExecuteDelegate(e.Parameter);
		}

		/// <summary>Method for CommandBinding.ExecuteRouted.</summary>
		/// <param name="sender">The command target that is invoking the handler.</param>
		/// <param name="e">The event data.</param>
		protected virtual void ExecuteRoutedMethod(object sender, ExecutedRoutedEventArgs e)
		{
			e.Handled = Handled;
			executeDelegate(e.Parameter);
		}
		#endregion

		#region Callback methods Declaration

		/// <summary>Static Callback Method When Changing RoutedCommand Value</summary>
		/// <param name="d">The object in which the value has changed</param>
		/// <param name="e">Change parameters</param>
		private static void RoutedCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
			=> ((RoutedCommandBinding)d).CommandBinding.Command = (RoutedCommand)e.NewValue;

		/// <summary>Static Callback Method When Changing Command Value</summary>
		/// <param name="d">The object in which the value has changed</param>
		/// <param name="e">Change parameters</param>
		private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RoutedCommandBinding dd = (RoutedCommandBinding)d;
			if (e.NewValue is ICommand newCommand)
			{
				dd.canExecuteDelegate = newCommand.CanExecute;
				dd.executeDelegate = newCommand.Execute;
			}
			else
			{
				dd.canExecuteDelegate = CanExecuteDefault;
				dd.executeDelegate = ExecuteDefault;
			}
		}
		#endregion
	}
}
