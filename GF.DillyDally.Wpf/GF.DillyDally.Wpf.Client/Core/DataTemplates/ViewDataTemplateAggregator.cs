using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using GF.DillyDally.Mvvmc.Contracts;
using GF.DillyDally.Wpf.Client.Core.ApplicationState;
using GF.DillyDally.Wpf.Client.Core.Exceptions;

namespace GF.DillyDally.Wpf.Client.Core.DataTemplates
{
    internal sealed class ViewDataTemplateAggregator
    {
        private static readonly Type ViewForTagType = typeof(IViewFor<>);
        private static readonly Type ViewModelConventionType = typeof(IViewModel);

        private readonly DataTemplateDictionaryFactory _dataTemplateDictionaryFactory =
            new DataTemplateDictionaryFactory();

        private readonly DataTemplateFactory _dataTemplateFactory = new DataTemplateFactory();

        private object TryFindResource(Type associatedType, IApplicationRuntime application)
        {
            var key = new DataTemplateKey(associatedType);
            return application.TryFindResource(key);
        }

        private IEnumerable<Type> FindViewMatchesForViewModel(Type viewModelType, IList<Type> viewTypes)
        {
            var genericInterfaces = ViewForTagType.MakeGenericType(viewModelType);
            var viewCandidates = viewTypes.Where(viewType => genericInterfaces.IsAssignableFrom(viewType));
            return viewCandidates;
        }

        internal ResourceDictionary CreateDataTemplateDictionaryForViewModelsInAssembly(Assembly assembly)
        {
            var viewModelTypes = assembly.GetExportedTypes().Where(this.TypeRespectsViewModelConvention).ToList();
            var viewTypes = assembly.GetExportedTypes().Where(this.TypeRespectsViewConvention).ToList();

            var viewModelViewCombinations = new Dictionary<Type, Type>();
            foreach (var viewModelType in viewModelTypes)
            {
                var viewTypeCandidates = this.FindViewMatchesForViewModel(viewModelType, viewTypes).ToList();

                if (viewTypeCandidates.Count > 0)
                {
                    if (viewTypeCandidates.Count > 1)
                    {
                        throw new MultipleViewDefinitionException(
                            $"Found more than one View match for a single ViewModel. Check Namingconventions. ViewModel - {viewModelType}");
                    }

                    var viewTypeMatch = viewTypeCandidates.First();

                    viewModelViewCombinations.Add(viewModelType, viewTypeMatch);
                }
            }

            return this._dataTemplateDictionaryFactory.CreateDataTemplateDictionaryForViewModelsInAssembly(assembly,
                viewModelViewCombinations);
        }

        internal IList<DataTemplate> CreateDataTemplatesForViewModelsInAssembly(Assembly assembly,
            IApplicationRuntime application)
        {
            var viewModelTypes = assembly.GetExportedTypes().Where(this.TypeRespectsViewModelConvention).ToList();
            var viewTypes = assembly.GetExportedTypes().Where(this.TypeRespectsViewConvention).ToList();
            var viewModelTypesWithoutDataTemplate =
                viewModelTypes.Where(type => !this.HasDataTemplateResourceForType(type, application)).ToList();

            if (!viewModelTypesWithoutDataTemplate.Any())
            {
                return new List<DataTemplate>();
            }

            return this.CreateDataTemplatesFor(viewModelTypesWithoutDataTemplate, viewTypes);
        }

        private IList<DataTemplate> CreateDataTemplatesFor(IList<Type> viewModelTypesWithoutDataTemplate,
            IList<Type> viewTypes)
        {
            var generatedViewModelDataTemplates = new List<DataTemplate>();
            foreach (var viewModelType in viewModelTypesWithoutDataTemplate)
            {
                var viewTypeCandidates = this.FindViewMatchesForViewModel(viewModelType, viewTypes).ToList();

                if (viewTypeCandidates.Count > 0)
                {
                    if (viewTypeCandidates.Count > 1)
                    {
                        throw new MultipleViewDefinitionException(
                            $"Found more than one View match for a single ViewModel. Check Namingconventions. ViewModel - {viewModelType}");
                    }

                    var viewTypeMatch = viewTypeCandidates.First();
                    var createdGenericDataTemplate =
                        this._dataTemplateFactory.CreateViewModelDataTemplate(viewModelType, viewTypeMatch);
                    generatedViewModelDataTemplates.Add(createdGenericDataTemplate);
                }
            }

            return generatedViewModelDataTemplates;
        }

        private bool HasDataTemplateResourceForType(Type type, IApplicationRuntime application)
        {
            return this.TryFindResource(type, application) != null;
        }

        private bool TypeRespectsViewConvention(Type type)
        {
            return
                type.GetInterfaces()
                    .Any(intf =>
                        intf.IsGenericType && ViewForTagType.IsAssignableFrom(intf.GetGenericTypeDefinition()));
        }

        private bool TypeRespectsViewModelConvention(Type type)
        {
            var isClass = type.IsClass;
            var isNotAbstract = !type.IsAbstract;
            var isNotInterface = !type.IsInterface;
            return isClass && isNotAbstract && isNotInterface && ViewModelConventionType.IsAssignableFrom(type);
        }
    }
}