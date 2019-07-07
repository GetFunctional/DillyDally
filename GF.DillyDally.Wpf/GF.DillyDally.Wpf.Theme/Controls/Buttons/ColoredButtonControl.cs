using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Theme.Controls.Buttons
{
    [DesignTimeVisible(true)]
    public class ColoredButtonControl : Button
    {
        public static readonly DependencyProperty ButtonColorProperty = DependencyProperty.Register(
            "ButtonColor", typeof(ButtonColor), typeof(ColoredButtonControl),
            new PropertyMetadata(default(ButtonColor)));

        static ColoredButtonControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ColoredButtonControl), new FrameworkPropertyMetadata(typeof(ColoredButtonControl)));
        }

        public ButtonColor ButtonColor
        {
            get { return (ButtonColor) this.GetValue(ButtonColorProperty); }
            set { this.SetValue(ButtonColorProperty, value); }
        }
    }
}