using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Theme.Controls.Input
{
    [DesignTimeVisible(true)]
    public class ImageInputControl : Control
    {
        public static readonly DependencyProperty ImageBytesProperty = DependencyProperty.Register(
            "ImageBytes", typeof(IList<byte>), typeof(ImageInputControl), new PropertyMetadata(default(IList<byte>)));

        public static readonly DependencyProperty NoImageControlTemplateProperty = DependencyProperty.Register(
            "NoImageControlTemplate", typeof(ControlTemplate), typeof(ImageInputControl),
            new PropertyMetadata(default(ControlTemplate)));

        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly",
            typeof(bool), typeof(ImageInputControl), new PropertyMetadata(default(bool)));

        static ImageInputControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ImageInputControl), new FrameworkPropertyMetadata(typeof(ImageInputControl)));
        }

        public ControlTemplate NoImageControlTemplate
        {
            get { return (ControlTemplate) this.GetValue(NoImageControlTemplateProperty); }
            set { this.SetValue(NoImageControlTemplateProperty, value); }
        }

        [Bindable(true)]
        public IList<byte> ImageBytes
        {
            get { return (IList<byte>) this.GetValue(ImageBytesProperty); }
            set { this.SetValue(ImageBytesProperty, value); }
        }

        [Bindable(true)]
        public bool IsReadOnly
        {
            get { return (bool) this.GetValue(IsReadOnlyProperty); }
            set { this.SetValue(IsReadOnlyProperty, value); }
        }
    }
}