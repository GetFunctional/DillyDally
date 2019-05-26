using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using GF.DillyDally.Wpf.Theme.Controls.Shared;

namespace GF.DillyDally.Wpf.Theme.Controls.Layout
{
    [DesignTimeVisible(true)]
    public class FormElementGroupControl : ContentControl
    {
        public static readonly DependencyProperty HeaderTitleContentProperty = DependencyProperty.Register(
            "HeaderTitleContent", typeof(HeaderTitleContent), typeof(FormElementGroupControl), new PropertyMetadata(default(HeaderTitleContent)));

        public static readonly DependencyProperty HeaderContentTemplateProperty = DependencyProperty.Register(
            "HeaderContentTemplate", typeof(DataTemplate), typeof(FormElementGroupControl), new PropertyMetadata(default(DataTemplate)));

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

        public DataTemplate HeaderContentTemplate
        {
            get
            {
                return (DataTemplate)this.GetValue(HeaderContentTemplateProperty);
            }
            set
            {
                this.SetValue(HeaderContentTemplateProperty, value);
            }
        }
    }
}