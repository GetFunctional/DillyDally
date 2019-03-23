using System.Reflection;
using System.Windows;

namespace GF.DillyDally.Wpf.Client.Core.DataTemplates
{
    internal sealed class DataTemplateInitializer
    {
        internal void RegisterDataTemplates(Application application)
        {
            var dataTemplateAggregator = new ViewDataTemplateAggregator();
            var dataTemplates =
                dataTemplateAggregator.CreateDataTemplatesForViewModelsInAssembly(
                    typeof(DataTemplateInitializer).GetTypeInfo().Assembly, application);

            foreach (var dataTemplate in dataTemplates)
            {
                application.Resources.Add(dataTemplate.DataTemplateKey, dataTemplate);
            }
        }
    }
}