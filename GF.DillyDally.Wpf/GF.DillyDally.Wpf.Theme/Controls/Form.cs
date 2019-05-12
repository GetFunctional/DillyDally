using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Theme.Controls
{
    [DesignTimeVisible(true)]
    public class Form : ContentControl
    {
        static Form()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(Form), new FrameworkPropertyMetadata(typeof(Form)));
        }

        public static readonly DependencyProperty HeaderTitleProperty = DependencyProperty.Register(
            "HeaderTitle", typeof(string), typeof(Form), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty HeaderSubtitleProperty = DependencyProperty.Register(
            "HeaderSubtitle", typeof(string), typeof(Form), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty FooterContentProperty = DependencyProperty.Register(
            "FooterContent", typeof(object), typeof(Form), new PropertyMetadata(default(object)));

        public string HeaderTitle
        {
            get { return (string) this.GetValue(HeaderTitleProperty); }
            set { this.SetValue(HeaderTitleProperty, value); }
        }

        public string HeaderSubtitle
        {
            get { return (string) this.GetValue(HeaderSubtitleProperty); }
            set { this.SetValue(HeaderSubtitleProperty, value); }
        }

        public object FooterContent
        {
            get { return this.GetValue(FooterContentProperty); }
            set { this.SetValue(FooterContentProperty, value); }
        }
    }
}