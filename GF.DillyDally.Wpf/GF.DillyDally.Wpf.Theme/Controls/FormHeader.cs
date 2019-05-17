using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using GF.DillyDally.Wpf.Theme.Controls.Shared;

namespace GF.DillyDally.Wpf.Theme.Controls
{
    [DesignTimeVisible(true)]
    public class FormHeader : Control
    {
        public static readonly DependencyProperty HeaderTitleContentProperty = DependencyProperty.Register(
            "HeaderTitleContent", typeof(HeaderTitleContent), typeof(FormHeader), new PropertyMetadata(default(HeaderTitleContent)));

        public static readonly DependencyProperty HeaderTitleContentTemplateProperty = DependencyProperty.Register(
            "HeaderTitleContentTemplate", typeof(DataTemplate), typeof(FormHeader), new PropertyMetadata(default(DataTemplate)));

        static FormHeader()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(FormHeader), new FrameworkPropertyMetadata(typeof(FormHeader)));
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

        public DataTemplate HeaderTitleContentTemplate
        {
            get
            {
                return (DataTemplate)this.GetValue(HeaderTitleContentTemplateProperty);
            }
            set
            {
                this.SetValue(HeaderTitleContentTemplateProperty, value);
            }
        }
    }
}