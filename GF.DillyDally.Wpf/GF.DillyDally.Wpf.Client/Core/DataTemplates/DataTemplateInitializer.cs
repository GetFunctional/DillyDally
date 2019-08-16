using System.Diagnostics;
using System.Reflection;
using GF.DillyDally.Wpf.Client.Core.ApplicationState;
using log4net;

namespace GF.DillyDally.Wpf.Client.Core.DataTemplates
{
    internal sealed class DataTemplateInitializer
    {
        private static readonly ILog DataTemplateInitializerLogger = LogManager.GetLogger(typeof(DataTemplateInitializer));

        internal void RegisterDataTemplates(IApplicationRuntime application)
        {
            var dataTemplateAggregator = new ViewDataTemplateAggregator();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var dataTemplateDictionary = dataTemplateAggregator.CreateDataTemplateDictionaryForViewModelsInAssembly(this.GetType().GetTypeInfo().Assembly);
            stopWatch.Stop();
            DataTemplateInitializerLogger.DebugFormat("DataTemplates initialized in {0} ms.", stopWatch.ElapsedMilliseconds);

            if (dataTemplateDictionary.Count > 0)
            {
                application.AddResource(dataTemplateDictionary);
            }
        }
    }
}