using System;
using System.Globalization;
using System.Windows.Data;

namespace GF.DillyDally.Wpf.Theme.Converter
{
    [ValueConversion(typeof(object), typeof(bool))]
    public sealed class IsNullToBooleanConverter : IValueConverter
    {
        public bool IsInverted { get; set; }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.IsInverted)
            {
                return value != null;
            }

            return value == null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("No reverse conversion supported");
        }

        #endregion
    }
}