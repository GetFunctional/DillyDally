using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Theme.Controls
{
    [DesignTimeVisible(true)]
    public class ImageInputControl : Control
    {
        static ImageInputControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ImageInputControl), new FrameworkPropertyMetadata(typeof(ImageInputControl)));
        }
    }
}