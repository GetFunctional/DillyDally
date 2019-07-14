using System.Windows;
using System.Windows.Controls;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create.Fields
{
    public class ActivityFieldsItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate AddNewFieldDataTemplate { get; set; }

        public DataTemplate ActivityFieldItemViewModel { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is AddNewFieldViewModel)
            {
                return this.AddNewFieldDataTemplate;
            }

            if (item is ActivityFieldItemViewModel)
            {
                return this.ActivityFieldItemViewModel;
            }

            return base.SelectTemplate(item, container);
        }
    }
}