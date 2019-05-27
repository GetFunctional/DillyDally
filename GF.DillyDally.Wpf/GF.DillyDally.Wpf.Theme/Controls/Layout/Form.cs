using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Theme.Controls.Layout
{
    [DesignTimeVisible(true)]
    public class Form : ContentControl
    {
        public static readonly DependencyProperty FooterContentProperty = DependencyProperty.Register(
            "FooterContent", typeof(object), typeof(Form), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(Form), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            "Description", typeof(string), typeof(Form), new PropertyMetadata(default(string)));

        static Form()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(Form), new FrameworkPropertyMetadata(typeof(Form)));
        }

        public string Title
        {
            get
            {
                return (string)this.GetValue(TitleProperty);
            }
            set
            {
                this.SetValue(TitleProperty, value);
            }
        }

        public string Description
        {
            get
            {
                return (string)this.GetValue(DescriptionProperty);
            }
            set
            {
                this.SetValue(DescriptionProperty, value);
            }
        }

        public object FooterContent
        {
            get
            {
                return this.GetValue(FooterContentProperty);
            }
            set
            {
                this.SetValue(FooterContentProperty, value);
            }
        }
    }
}