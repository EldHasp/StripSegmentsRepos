using System.Windows;
using System.Windows.Input;

namespace AttachedProperties
{
	/// <summary>Creating CommandBinding on Received RoutedCommand and ICommand</summary>
	public partial class RoutedCommandBinding : Freezable
	{
		#region Property Declaration
		/// <summary>Binding for an popup RoutedCommand</summary>
		public RoutedCommand RoutedCommand
		{
			get { return (RoutedCommand)GetValue(RoutedCommandProperty); }
			set { SetValue(RoutedCommandProperty, value); }
		}

		// Using a DependencyProperty as the backing store for RoutedCommand.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty RoutedCommandProperty =
			DependencyProperty.Register(nameof(RoutedCommand), typeof(RoutedCommand), typeof(RoutedCommandBinding),
				new PropertyMetadata(null, RoutedCommandChanged));

		/// <summary>Binding for an executable ICommand</summary>
		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(RoutedCommandBinding),
				new PropertyMetadata(null, CommandChanged));

		/// <summary>Binding for an Handled completion</summary>
		public bool Handled
		{
			get { return (bool)GetValue(HandledProperty); }
			set { SetValue(HandledProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Handled.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty HandledProperty =
			DependencyProperty.Register(nameof(Handled), typeof(bool), typeof(RoutedCommandBinding), new PropertyMetadata(true));

		/// <summary>Customized Instance CommandBinding</summary>
		public CommandBinding CommandBinding { get; }
		public static RoutedCommand EmptyCommand { get; } = new RoutedUICommand("Empty", nameof(EmptyCommand),typeof(RoutedCommandBinding));

		public RoutedCommandBinding()
		{
			CommandBinding = new CommandBinding(EmptyCommand, ExecuteRoutedMethod, CanExecuteRoutedMethod);
		}

		protected override Freezable CreateInstanceCore()
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}
}
