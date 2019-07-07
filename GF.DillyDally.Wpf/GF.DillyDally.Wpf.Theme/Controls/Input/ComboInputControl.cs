using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Theme.Controls.Input
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

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof(object), typeof(ComboInputControl), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            "ItemTemplate", typeof(DataTemplate), typeof(ComboInputControl),
            new PropertyMetadata(default(DataTemplate)));

        static ComboInputControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ComboInputControl), new FrameworkPropertyMetadata(typeof(ComboInputControl)));
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate) this.GetValue(ItemTemplateProperty); }
            set { this.SetValue(ItemTemplateProperty, value); }
        }

        [Bindable(true)]
        public object InputValue
        {
            get { return this.GetValue(InputValueProperty); }
            set { this.SetValue(InputValueProperty, value); }
        }

        [Bindable(true)]
        public string WatermarkText
        {
            get { return (string) this.GetValue(WatermarkTextProperty); }
            set { this.SetValue(WatermarkTextProperty, value); }
        }

        [Bindable(true)]
        public object Label
        {
            get { return this.GetValue(LabelProperty); }
            set { this.SetValue(LabelProperty, value); }
        }

        [Bindable(true)]
        public object ItemsSource
        {
            get { return this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }
    }
}