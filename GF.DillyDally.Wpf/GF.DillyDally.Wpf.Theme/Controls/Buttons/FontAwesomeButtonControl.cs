using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using FontAwesome5;

namespace GF.DillyDally.Wpf.Theme.Controls.Buttons
{
    [DesignTimeVisible(true)]
    public class FontAwesomeButtonControl : Button
    {
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon", typeof(EFontAwesomeIcon), typeof(FontAwesomeButtonControl),
            new PropertyMetadata(default(EFontAwesomeIcon)));

        static FontAwesomeButtonControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(FontAwesomeButtonControl), new FrameworkPropertyMetadata(typeof(FontAwesomeButtonControl)));
        }

        public EFontAwesomeIcon Icon
        {
            get { return (EFontAwesomeIcon) this.GetValue(IconProperty); }
            set { this.SetValue(IconProperty, value); }
        }
    }
}