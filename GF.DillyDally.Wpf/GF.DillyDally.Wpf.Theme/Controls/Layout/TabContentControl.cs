using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Theme.Controls.Layout
{
    [DesignTimeVisible(true)]
    public class TabContentControl : Control
    {
        public static readonly DependencyProperty SelectedTabProperty = DependencyProperty.Register(
            "SelectedTab", typeof(ITabItem), typeof(TabContentControl), new PropertyMetadata(default(ITabItem)));

        public static readonly DependencyProperty TabItemContentTemplateProperty = DependencyProperty.Register(
            "TabItemContentTemplate", typeof(DataTemplate), typeof(TabContentControl), new PropertyMetadata(default(DataTemplate)));

        public static readonly DependencyProperty TabItemHeaderTemplateProperty = DependencyProperty.Register(
            "TabItemHeaderTemplate", typeof(DataTemplate), typeof(TabContentControl), new PropertyMetadata(default(DataTemplate)));

        public static readonly DependencyProperty TabItemHeaderTemplateSelectorProperty = DependencyProperty.Register(
            "TabItemHeaderTemplateSelector", typeof(DataTemplateSelector), typeof(TabContentControl), new PropertyMetadata(default(DataTemplateSelector)));

        public static readonly DependencyProperty TabItemContentTemplateSelectorProperty = DependencyProperty.Register(
            "TabItemContentTemplateSelector", typeof(DataTemplateSelector), typeof(TabContentControl), new PropertyMetadata(default(DataTemplateSelector)));

        public static readonly DependencyProperty TabItemsSourceProperty = DependencyProperty.Register(
            "TabItemsSource", typeof(object), typeof(TabContentControl), new PropertyMetadata(default(object)));

        static TabContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(TabContentControl), new FrameworkPropertyMetadata(typeof(TabContentControl)));
        }

        public DataTemplate TabItemContentTemplate
        {
            get
            {
                return (DataTemplate)this.GetValue(TabItemContentTemplateProperty);
            }
            set
            {
                this.SetValue(TabItemContentTemplateProperty, value);
            }
        }

        public DataTemplate TabItemHeaderTemplate
        {
            get
            {
                return (DataTemplate)this.GetValue(TabItemHeaderTemplateProperty);
            }
            set
            {
                this.SetValue(TabItemHeaderTemplateProperty, value);
            }
        }

        public DataTemplateSelector TabItemHeaderTemplateSelector
        {
            get
            {
                return (DataTemplateSelector)this.GetValue(TabItemHeaderTemplateSelectorProperty);
            }
            set
            {
                this.SetValue(TabItemHeaderTemplateSelectorProperty, value);
            }
        }

        public DataTemplateSelector TabItemContentTemplateSelector
        {
            get
            {
                return (DataTemplateSelector)this.GetValue(TabItemContentTemplateSelectorProperty);
            }
            set
            {
                this.SetValue(TabItemContentTemplateSelectorProperty, value);
            }
        }

        public object TabItemsSource
        {
            get
            {
                return this.GetValue(TabItemsSourceProperty);
            }
            set
            {
                this.SetValue(TabItemsSourceProperty, value);
            }
        }

        public ITabItem SelectedTab
        {
            get
            {
                return (ITabItem)this.GetValue(SelectedTabProperty);
            }
            set
            {
                this.SetValue(SelectedTabProperty, value);
            }
        }
    }
}