using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Theme.Controls.Buttons
{
    public class ChromelessButtonControl : Button
    {
        static ChromelessButtonControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ChromelessButtonControl), new FrameworkPropertyMetadata(typeof(ChromelessButtonControl)));
        }
    }
}