using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using GF.DillyDally.Wpf.Client.Core.Dialoge;

namespace GF.DillyDally.Wpf.Client.Presentation
{
    [ValueConversion(typeof(DialogSize), typeof(GridLength))]
    public sealed class OverlaySizeToGridLengthConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dialogSize = (DialogSize) value;

            var bigSize = 16.5d;
            var mediumSize = 25d;
            var smallSize = 33d;

            switch (dialogSize)
            {
                case DialogSize.Small:
                    return new GridLength(smallSize , GridUnitType.Star);
                case DialogSize.Medium:
                    return new GridLength(mediumSize, GridUnitType.Star);
                case DialogSize.Big:
                    return new GridLength(bigSize, GridUnitType.Star);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("No reverse conversion supported");
        }

        #endregion
    }
}