using Models;
using System.Windows;

namespace StripSegments
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {// В этой строке точка останова. После останова дальше по F11.
            string fileName = "strips.xml";
            StripsModel model = new StripsModel(fileName);
            model.Load();
        }

    }
}
