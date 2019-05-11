using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;

namespace GF.DillyDally.Wpf.Theme.Extensions
{
    [ContentProperty("Size")]
    [MarkupExtensionReturnType(typeof(double))]
    public sealed class FontSizeExtension : MarkupExtension
    {
        [TypeConverter(typeof(FontSizeConverter))]
        public double Size { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this.Size;
        }
    }
}