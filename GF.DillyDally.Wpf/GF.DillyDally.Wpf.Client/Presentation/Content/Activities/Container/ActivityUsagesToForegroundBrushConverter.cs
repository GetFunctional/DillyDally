using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace GF.DillyDally.Wpf.Client
{
    [ValueConversion(typeof(int), typeof(Brush))]
    public sealed class ActivityUsagesToForegroundBrushConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var usagesValue = (int)value;

            if (usagesValue < 5)
            {
                return this.LessThan5Brush;
            }

            if (usagesValue < 10)
            {
                return this.LessThan10Brush;
            }

            if (usagesValue < 25)
            {
                return this.LessThan25Brush;
            }

            if (usagesValue < 50)
            {
                return this.LessThan50Brush;
            }

            return this.LessThan100Brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("No reverse conversion supported");
        }

        #endregion

        public Brush LessThan5Brush { get; set; }
        public Brush LessThan10Brush { get; set; }
        public Brush LessThan25Brush { get; set; }
        public Brush LessThan50Brush { get; set; }
        public Brush LessThan100Brush { get; set; }
    }
}