using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace StripSegments
{
    /// <summary>
    /// Логика взаимодействия для StripUC.xaml
    /// </summary>
    public partial class StripUC : UserControl
    {
        public StripUC()
        {
            InitializeComponent();
        }

        /// <summary>Источник последовательности Сегментов.</summary>
        public IEnumerable<StripSegment> SegmentsSource
        {
            get { return (IEnumerable<StripSegment>)GetValue(SegmentsSourceProperty); }
            set { SetValue(SegmentsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SegmentsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SegmentsSourceProperty =
            DependencyProperty.Register(nameof(SegmentsSource), typeof(IEnumerable<StripSegment>), typeof(StripUC), new PropertyMetadata(null));
    }
}
