using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using GF.DillyDally.Wpf.Theme.Controls.Shared;

namespace GF.DillyDally.Wpf.Theme.Controls.Layout
{
    [DesignTimeVisible(true)]
    public class Form : ContentControl
    {
        public static readonly DependencyProperty HeaderTitleContentProperty = DependencyProperty.Register(
            "HeaderTitleContent", typeof(HeaderTitleContent), typeof(Form), new PropertyMetadata(default(HeaderTitleContent)));

        public static readonly DependencyProperty FooterContentProperty = DependencyProperty.Register(
            "FooterContent", typeof(object), typeof(Form), new PropertyMetadata(default(object)));

        static Form()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(Form), new FrameworkPropertyMetadata(typeof(Form)));
        }

        public HeaderTitleContent HeaderTitleContent
        {
            get
            {
                return (HeaderTitleContent)this.GetValue(HeaderTitleContentProperty);
            }
            set
            {
                this.SetValue(HeaderTitleContentProperty, value);
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