using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Theme.Controls
{
    [DesignTimeVisible(true)]
    public class ImageEditorControl : Control
    {
       
        static ImageEditorControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ImageEditorControl), new FrameworkPropertyMetadata(typeof(ImageEditorControl)));
        }
    }
}