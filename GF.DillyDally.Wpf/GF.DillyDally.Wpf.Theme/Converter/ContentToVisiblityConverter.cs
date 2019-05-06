using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GF.DillyDally.Wpf.Theme.Converter
{
    [ValueConversion(typeof(object), typeof(Visibility))]
    public sealed class ContentToVisiblityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("No reverse conversion supported");
        }

        #endregion
    }
}