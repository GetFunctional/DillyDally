using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Theme.Controls
{
    [DesignTimeVisible(true)]
    public class ColoredButtonControl : Button
    {
        public static readonly DependencyProperty ButtonColorProperty = DependencyProperty.Register(
            "ButtonColor", typeof(ButtonColor), typeof(ColoredButtonControl), new PropertyMetadata(default(ButtonColor)));

        public ButtonColor ButtonColor
        {
            get
            {
                return (ButtonColor)this.GetValue(ButtonColorProperty);
            }
            set
            {
                this.SetValue(ButtonColorProperty, value);
            }
        }
    }
}