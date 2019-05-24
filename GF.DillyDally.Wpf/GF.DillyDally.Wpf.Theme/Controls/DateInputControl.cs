using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Theme.Controls
{
    [DesignTimeVisible(true)]
    public class DateInputControl : Control
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            "Label", typeof(object), typeof(DateInputControl), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty WatermarkTextProperty = DependencyProperty.Register(
            "WatermarkText", typeof(string), typeof(DateInputControl), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty InputValueProperty = DependencyProperty.Register(
            "InputValue", typeof(DateTime?), typeof(DateInputControl), new PropertyMetadata(default(DateTime?)));

        static DateInputControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(DateInputControl), new FrameworkPropertyMetadata(typeof(DateInputControl)));
        }

        [Bindable(true)]
        public string WatermarkText
        {
            get { return (string) this.GetValue(WatermarkTextProperty); }
            set { this.SetValue(WatermarkTextProperty, value); }
        }

        [Bindable(true)]
        public DateTime? InputValue
        {
            get { return (DateTime?) this.GetValue(InputValueProperty); }
            set { this.SetValue(InputValueProperty, value); }
        }

        [Bindable(true)]
        public object Label
        {
            get { return this.GetValue(LabelProperty); }
            set { this.SetValue(LabelProperty, value); }
        }
    }
}