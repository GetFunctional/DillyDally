using System.Reflection;
using System.Windows;

namespace GF.DillyDally.Wpf.Client.Core
{
    internal sealed class DataTemplateInitializer
    {
        #region - Methoden privat -

        internal void RegisterDataTemplates(Application application)
        {
            var dataTemplateAggregator = new ViewDataTemplateAggregator();
            var dataTemplates =
                dataTemplateAggregator.CreateDataTemplatesForViewModelsInAssembly(
                    typeof(Bootstrapper).GetTypeInfo().Assembly, application);

            foreach (var dataTemplate in dataTemplates)
            {
                application.Resources.Add(dataTemplate.DataTemplateKey, dataTemplate);
            }
        }

        #endregion
    }
}