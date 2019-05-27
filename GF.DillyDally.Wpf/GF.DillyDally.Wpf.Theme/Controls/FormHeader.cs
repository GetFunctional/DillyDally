using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Theme.Controls
{
    [DesignTimeVisible(true)]
    public class FormHeader : Control
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(FormHeader), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            "Description", typeof(string), typeof(FormHeader), new PropertyMetadata(default(string)));

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
    }
}