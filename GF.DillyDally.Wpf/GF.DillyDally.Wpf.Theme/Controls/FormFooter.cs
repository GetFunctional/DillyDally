using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Theme.Controls
{
    [DesignTimeVisible(true)]
    public class FormFooter : ContentControl
    {
        static FormFooter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(FormFooter), new FrameworkPropertyMetadata(typeof(FormFooter)));
        }
        }
}