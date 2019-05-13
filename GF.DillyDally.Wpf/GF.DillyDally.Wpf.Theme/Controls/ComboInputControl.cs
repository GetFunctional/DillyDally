using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Theme.Controls
{
    [DesignTimeVisible(true)]
    public class ComboInputControl : Control
    {
        public static readonly DependencyProperty InputValueProperty = DependencyProperty.Register(
            "InputValue", typeof(object), typeof(ComboInputControl), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            "Label", typeof(object), typeof(ComboInputControl), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty WatermarkTextProperty = DependencyProperty.Register(
            "WatermarkText", typeof(string), typeof(ComboInputControl), new PropertyMetadata(default(string)));

        static ComboInputControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ComboInputControl), new FrameworkPropertyMetadata(typeof(ComboInputControl)));
        }

        public object InputValue
        {
            get { return this.GetValue(InputValueProperty); }
            set { this.SetValue(InputValueProperty, value); }
        }

        public string WatermarkText
        {
            get { return (string) this.GetValue(WatermarkTextProperty); }
            set { this.SetValue(WatermarkTextProperty, value); }
        }

        public object Label
        {
            get { return this.GetValue(LabelProperty); }
            set { this.SetValue(LabelProperty, value); }
        }
    }
}