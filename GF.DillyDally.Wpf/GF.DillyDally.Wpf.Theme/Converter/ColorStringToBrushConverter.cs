using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace GF.DillyDally.Wpf.Theme.Converter
{
    [ValueConversion(typeof(string), typeof(SolidColorBrush))]
    public sealed class ColorStringToBrushConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hexColor = value.ToString();

            return new BrushConverter().ConvertFrom(hexColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("No reverse conversion supported");
        }

        #endregion
    }
}