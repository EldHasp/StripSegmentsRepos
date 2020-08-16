using Models;
using System.Windows;

namespace StripSegments
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string fileName = "strips.xml";
            StripsModel model = new StripsModel(fileName);

            StripsViewModel viewModel = new StripsViewModel(model);

            MainWindow = new MainWindow() { DataContext = viewModel };

            MainWindow.Show();

            viewModel.Load();
        }
    }
}
