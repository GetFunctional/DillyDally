using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Theme.Controls.Layout
{
    public class NavigationTargetContentContainer : ContentControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(NavigationTargetContentContainer), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(NavigationTargetContentContainer),
                new PropertyMetadata(default(bool)));

        static NavigationTargetContentContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(NavigationTargetContentContainer),
                new FrameworkPropertyMetadata(typeof(NavigationTargetContentContainer)));
        }

        public string Title
        {
            get { return (string) this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }

        public bool IsBusy
        {
            get { return (bool) this.GetValue(IsBusyProperty); }
            set { this.SetValue(IsBusyProperty, value); }
        }
    }
}