using System;
using System.Windows;
using System.Windows.Markup;

namespace GF.DillyDally.Wpf.Client.Core
{
    internal sealed class DataTemplateFactory
    {
        private const string defaultDataTemplateSchema = @"<DataTemplate DataType=""{{x:Type vm:{0}}}"">
<v:{1}>
</v:{1}>
</DataTemplate>";

        public DataTemplateFactory() : this(defaultDataTemplateSchema)
        {
        }

        public DataTemplateFactory(string dataTemplateSchema)
        {
            this.DataTemplateSchema = dataTemplateSchema;
        }

        private string DataTemplateSchema { get; }

        internal DataTemplate CreateViewModelDataTemplate(Type viewModelType, Type viewType)
        {
            var xaml = string.Format(this.DataTemplateSchema, viewModelType.Name, viewType.Name);
            var context = new ParserContext();

            context.XamlTypeMapper = new XamlTypeMapper(new string[0]);
            context.XamlTypeMapper.AddMappingProcessingInstruction("vm", viewModelType.Namespace,
                viewModelType.Assembly.GetName().Name);
            context.XamlTypeMapper.AddMappingProcessingInstruction("v", viewType.Namespace,
                viewType.Assembly.GetName().Name);

            context.XmlnsDictionary.Add(string.Empty, "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("dxmvvm", "http://schemas.devexpress.com/winfx/2008/xaml/mvvm");
            context.XmlnsDictionary.Add("vm", "vm");
            context.XmlnsDictionary.Add("v", "v");

            var template = (DataTemplate) XamlReader.Parse(xaml, context);
            return template;
        }
    }
}