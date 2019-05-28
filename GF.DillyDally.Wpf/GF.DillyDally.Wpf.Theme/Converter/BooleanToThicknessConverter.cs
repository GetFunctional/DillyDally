using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GF.DillyDally.Wpf.Theme.Converter
{
    [ValueConversion(typeof(bool), typeof(Thickness))]
    public sealed class BooleanToThicknessConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var booleanValue = (bool)value;

            return booleanValue ? new Thickness(0) : new Thickness(1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("No reverse conversion supported");
        }

        #endregion
    }
}