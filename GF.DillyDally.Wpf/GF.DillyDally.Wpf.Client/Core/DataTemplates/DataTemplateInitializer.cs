﻿using System.Reflection;
using System.Windows;
using GF.DillyDally.Wpf.Client.ApplicationState;

namespace GF.DillyDally.Wpf.Client.Core.DataTemplates
{
    internal sealed class DataTemplateInitializer
    {
        internal void RegisterDataTemplates(IApplicationRuntime application)
        {
            var dataTemplateAggregator = new ViewDataTemplateAggregator();
            var dataTemplates =
                dataTemplateAggregator.CreateDataTemplatesForViewModelsInAssembly(
                    typeof(DataTemplateInitializer).GetTypeInfo().Assembly, application);

            foreach (var dataTemplate in dataTemplates)
            {
                application.AddDataTemplate(dataTemplate.DataTemplateKey, dataTemplate);
            }
        }
    }
}