using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace StripSegments
{
    /// <summary>
    /// Логика взаимодействия для StripsUC.xaml
    /// </summary>
    public partial class StripsUC : UserControl
    {
        public StripsUC()
        {
            InitializeComponent();
        }

        /// <summary>Источник последовательностм полос.</summary>
        public IEnumerable<Strip> StripsSource
        {
            get { return (IEnumerable<Strip>)GetValue(StripsSourceProperty); }
            set { SetValue(StripsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StripsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StripsSourceProperty =
            DependencyProperty.Register(nameof(StripsSource), typeof(IEnumerable<Strip>), typeof(StripsUC), new PropertyMetadata(null));
    }
}
