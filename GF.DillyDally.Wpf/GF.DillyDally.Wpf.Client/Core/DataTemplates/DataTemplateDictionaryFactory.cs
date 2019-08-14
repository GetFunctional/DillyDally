using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Markup;

namespace GF.DillyDally.Wpf.Client.Core.DataTemplates
{
    internal sealed class DataTemplateDictionaryFactory
    {
        private readonly DataTemplateFactory _dataTemplateFactory = new DataTemplateFactory();

        private string GetProcessorNameFromNamespace(string valueNamespace)
        {
            return valueNamespace.Replace(".", "").ToLower();
        }

        private string CreateDataTemplatesXamlForDictionary(IReadOnlyDictionary<Type, Type> viewModelViewCombinations,
            Dictionary<Type, string> typeNamespaceProcessors)
        {
            var dataTemplatesXaml = new StringBuilder();
            foreach (var viewModelViewCombination in viewModelViewCombinations)
            {
                var dataTemplateXamlString =
                    this._dataTemplateFactory.CreateViewModelDataTemplateXamlString(viewModelViewCombination.Key,
                        viewModelViewCombination.Value, typeNamespaceProcessors);

                dataTemplatesXaml.AppendLine(dataTemplateXamlString);
            }

            return dataTemplatesXaml.ToString();
        }

        internal ResourceDictionary CreateDataTemplateDictionaryForViewModelsInAssembly(Assembly assembly,
            IReadOnlyDictionary<Type, Type> viewModelViewCombinations)
        {
            if (!viewModelViewCombinations.Any())
            {
                return new ResourceDictionary();
            }

            var typeNamespaceProcessors = viewModelViewCombinations.Keys.Concat(viewModelViewCombinations.Values)
                .ToDictionary(key => key, value => this.GetProcessorNameFromNamespace(value.Namespace));

            var dataTemplatesXaml =
                this.CreateDataTemplatesXamlForDictionary(viewModelViewCombinations, typeNamespaceProcessors);
            if (string.IsNullOrWhiteSpace(dataTemplatesXaml))
            {
                return new ResourceDictionary();
            }

            return this.CreateDictionaryWithDataTemplates(assembly, dataTemplatesXaml, typeNamespaceProcessors);
        }

        private XamlTypeMapper CreateXamlTypeMapper(Assembly assembly,
            IReadOnlyDictionary<Type, string> typeNamespaceProcessors)
        {
            var xamlTypeMapper = new XamlTypeMapper(new string[0]);

            foreach (var typeKeyValuePair in typeNamespaceProcessors)
            {
                xamlTypeMapper.AddMappingProcessingInstruction(typeKeyValuePair.Value, typeKeyValuePair.Key.Namespace,
                    assembly.GetName().Name);
            }

            return xamlTypeMapper;
        }

        private ResourceDictionary CreateDictionaryWithDataTemplates(Assembly assembly, string dataTemplatesXaml,
            Dictionary<Type, string> typeNamespaceProcessors)
        {
            var resourceDictionaryTemplate = string.Format(@"<ResourceDictionary>
{0}
</ResourceDictionary>", dataTemplatesXaml);
            var context = new ParserContext();
            var xamlTypeMapper = this.CreateXamlTypeMapper(assembly, typeNamespaceProcessors);
            context.XamlTypeMapper = xamlTypeMapper;
            context.XmlnsDictionary.Add(string.Empty, "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            foreach (var processor in typeNamespaceProcessors.Values.Distinct())
            {
                context.XmlnsDictionary.Add(processor, processor);
            }

            var template = (ResourceDictionary)XamlReader.Parse(resourceDictionaryTemplate, context);
            return template;
        }
    }
//}