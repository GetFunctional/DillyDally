using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Theme.Controls
{
    [DesignTimeVisible(true)]
    public class FormHeader : Control
    {
        static FormHeader()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(FormHeader), new FrameworkPropertyMetadata(typeof(FormHeader)));
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(FormHeader), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty SubtitleProperty = DependencyProperty.Register(
            "Subtitle", typeof(string), typeof(FormHeader), new PropertyMetadata(default(string)));

        public string Title
        {
            get { return (string) this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }

        public string Subtitle
        {
            get { return (string) this.GetValue(SubtitleProperty); }
            set { this.SetValue(SubtitleProperty, value); }
        }
    }
}