using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Markup;

namespace GF.DillyDally.Wpf.Client.Core.DataTemplates
{
    internal sealed class DataTemplateFactory
    {
        private const string DefaultDataTemplateSchema = @"<DataTemplate DataType=""{{x:Type {0}:{1}}}""><{2}:{3} /></DataTemplate>";
        
        internal DataTemplate CreateViewModelDataTemplate(Type viewModelType, Type viewType, string dataTemplateSchema = DefaultDataTemplateSchema)
        {
            var xaml = string.Format(dataTemplateSchema, "vm", viewModelType.Name, "v", viewType.Name);

            var xamlTypeMapper = CreateXamlTypeMapper(viewModelType, viewType);

            var context = new ParserContext();
            context.XamlTypeMapper = xamlTypeMapper;
            context.XmlnsDictionary.Add(string.Empty, "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("vm", "vm");
            context.XmlnsDictionary.Add("v", "v");

            var template = (DataTemplate) XamlReader.Parse(xaml, context);
            return template;
        }

        private static XamlTypeMapper CreateXamlTypeMapper(Type viewModelType, Type viewType)
        {
            var xamlTypeMapper = new XamlTypeMapper(new string[0]);
            xamlTypeMapper.AddMappingProcessingInstruction("vm", viewModelType.Namespace,
                viewModelType.Assembly.GetName().Name);
            xamlTypeMapper.AddMappingProcessingInstruction("v", viewType.Namespace,
                viewType.Assembly.GetName().Name);
            return xamlTypeMapper;
        }

        public string CreateViewModelDataTemplateXamlString(Type viewModelType, Type viewType,IReadOnlyDictionary<Type, string> typeNamespaceProcessors, string dataTemplateSchema = DefaultDataTemplateSchema)
        {
            var viewModelNamespacePrefix = typeNamespaceProcessors[viewModelType];
            var viewNamespacePrefix = typeNamespaceProcessors[viewType];

            var xaml = string.Format(dataTemplateSchema, viewModelNamespacePrefix, viewModelType.Name, viewNamespacePrefix, viewType.Name);
            return xaml;
        }
    }
}