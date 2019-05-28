using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Theme.Controls.Input
{
    [DesignTimeVisible(true)]
    public class ImageInputControl : Control
    {
        public static readonly DependencyProperty ImageBytesProperty = DependencyProperty.Register(
            "ImageBytes", typeof(byte[]), typeof(ImageInputControl), new PropertyMetadata(default(byte[])));

        static ImageInputControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ImageInputControl), new FrameworkPropertyMetadata(typeof(ImageInputControl)));
        }

        [Bindable(true)]
        public byte[] ImageBytes
        {
            get
            {
                return (byte[])this.GetValue(ImageBytesProperty);
            }
            set
            {
                this.SetValue(ImageBytesProperty, value);
            }
        }


    }
}