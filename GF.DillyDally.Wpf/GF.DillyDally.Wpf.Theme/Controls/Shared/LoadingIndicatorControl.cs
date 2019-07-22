using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Theme.Controls.Shared
{
    [DesignTimeVisible(true)]
    public class LoadingIndicatorControl : ContentControl
    {
        static LoadingIndicatorControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(LoadingIndicatorControl), new FrameworkPropertyMetadata(typeof(LoadingIndicatorControl)));
        }
    }
}