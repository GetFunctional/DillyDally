using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace GF.DillyDally.Wpf.Client.Core.DataTemplates
{
    internal sealed class DataTemplateFactory
    {
        private const string DefaultDataTemplateSchema = @"<DataTemplate DataType=""{{x:Type vm:{0}}}"">
<v:{1}>
</v:{1}>
</DataTemplate>";

        public DataTemplateFactory() : this(DefaultDataTemplateSchema)
        {
        }

        public DataTemplateFactory(string dataTemplateSchema)
        {
            this.DataTemplateSchema = dataTemplateSchema;
        }

        private string DataTemplateSchema { get; }

        [Obsolete("Certain WPF Binding functionality is lost using this method. F.e ElementBinding from elements that are not part of the visual tree.")]
        internal DataTemplate CreateViewModelDataTemplateFast(Type viewModelType, Type viewType)
        {
            var template = new DataTemplate(viewModelType);
            template.VisualTree = new FrameworkElementFactory(viewType);
            return template;
        }

        internal DataTemplate CreateViewModelDataTemplate(Type viewModelType, Type viewType)
        {
            var xaml = string.Format(this.DataTemplateSchema, viewModelType.Name, viewType.Name);

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
    }
}