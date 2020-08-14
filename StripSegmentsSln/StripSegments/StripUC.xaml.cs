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

        /// <summary>Полоса с данными.</summary>
        public Strip Strip
        {
            get { return (Strip)GetValue(StripProperty); }
            set { SetValue(StripProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Strip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StripProperty =
            DependencyProperty.Register(nameof(Strip), typeof(Strip), typeof(StripUC), new PropertyMetadata(null, StripChanged));

        private static void StripChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StripUC stripUC = (StripUC)d;
            stripUC.SegmentsSource = stripUC.Strip?.Segments;
        }

        /// <summary>Источник последовательности Сегментов.
        /// Свойство Только Для Чтения.</summary>
        public IEnumerable<Segment> SegmentsSource
        {
            get { return (IEnumerable<Segment>)GetValue(SegmentsSourceProperty); }
            private set { SetValue(SegmentsSourcePropertyKey, value); }
        }

        // Using a DependencyProperty as the backing store for SegmentsSource.  This enables animation, styling, binding, etc...
        private static readonly DependencyPropertyKey SegmentsSourcePropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(SegmentsSource), typeof(IEnumerable<Segment>), typeof(StripUC), new PropertyMetadata(null));
        public static readonly DependencyProperty SegmentsSourceProperty = SegmentsSourcePropertyKey.DependencyProperty;
    }

}
