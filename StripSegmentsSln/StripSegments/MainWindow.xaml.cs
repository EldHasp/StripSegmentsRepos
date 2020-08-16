using System.Windows;

namespace StripSegments
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LineLeftCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            StripsViewModelProperties viewModel = (StripsViewModelProperties)DataContext;
            e.CanExecute = viewModel.PrevStepCommand.CanExecute(null);
            e.Handled = true;
        }

        private void LineLeftCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            StripsViewModelProperties viewModel = (StripsViewModelProperties)DataContext;
            viewModel.PrevStepCommand.Execute(null);
            e.Handled = true;
        }

        private void LineRightCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            StripsViewModelProperties viewModel = (StripsViewModelProperties)DataContext;
            e.CanExecute = viewModel.NextStepCommand.CanExecute(null);
            e.Handled = true;
        }

        private void LineRightCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            StripsViewModelProperties viewModel = (StripsViewModelProperties)DataContext;
            viewModel.NextStepCommand.Execute(null);
            e.Handled = true;
        }
    }

}
