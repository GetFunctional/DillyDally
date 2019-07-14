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

        public static readonly DependencyProperty ButtonFormProperty = DependencyProperty.Register(
            "ButtonForm", typeof(ButtonForm), typeof(ColoredButtonControl), new PropertyMetadata(default(ButtonForm)));

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

        public ButtonForm ButtonForm
        {
            get { return (ButtonForm) this.GetValue(ButtonFormProperty); }
            set { this.SetValue(ButtonFormProperty, value); }
        }
    }
}