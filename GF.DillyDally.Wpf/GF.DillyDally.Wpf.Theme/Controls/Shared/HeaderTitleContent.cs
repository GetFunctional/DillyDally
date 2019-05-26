using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Theme.Controls.Shared
{
    public sealed class HeaderTitleContent : Control
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(HeaderTitleContent), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            "Description", typeof(string), typeof(HeaderTitleContent), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty HeaderTypeProperty = DependencyProperty.Register(
            "HeaderType", typeof(HeaderType), typeof(HeaderTitleContent), new PropertyMetadata(default(HeaderType)));

        static HeaderTitleContent()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(HeaderTitleContent), new FrameworkPropertyMetadata(typeof(HeaderTitleContent)));
        }

        public HeaderType HeaderType
        {
            get { return (HeaderType) this.GetValue(HeaderTypeProperty); }
            set { this.SetValue(HeaderTypeProperty, value); }
        }

        public string Title
        {
            get { return (string) this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }

        public string Description
        {
            get { return (string) this.GetValue(DescriptionProperty); }
            set { this.SetValue(DescriptionProperty, value); }
        }
    }
}