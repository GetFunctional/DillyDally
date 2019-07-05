using System;
using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.Shared.Extensions;
using LightInject;

namespace GF.DillyDally.Wpf.Client.Core.Ioc
{
    /// <summary>
    ///     An <see cref="IPropertyDependencySelector" /> that uses the <see cref="InjectAttribute" />
    ///     to determine which properties to inject dependencies.
    /// </summary>
    internal sealed class AnnotatedPropertyDependencySelector : PropertyDependencySelector
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AnnotatedPropertyDependencySelector" /> class.
        /// </summary>
        /// <param name="propertySelector">
        ///     The <see cref="IPropertySelector" /> that is
        ///     responsible for selecting a list of injectable properties.
        /// </param>
        internal AnnotatedPropertyDependencySelector(IPropertySelector propertySelector)
            : base(propertySelector)
        {
        }

        /// <summary>
        ///     Selects the property dependencies for the given <paramref name="type" />
        ///     that is annotated with the <see cref="InjectAttribute" />.
        /// </summary>
        /// <param name="type">The <see cref="Type" /> for which to select the property dependencies.</param>
        /// <returns>
        ///     A list of <see cref="PropertyDependency" /> instances that represents the property
        ///     dependencies for the given <paramref name="type" />.
        /// </returns>
        public override IEnumerable<PropertyDependency> Execute(Type type)
        {
            var properties = this.PropertySelector.Execute(type).Where(p => p.IsDefined(typeof(InjectAttribute), true)).ToArray();
            foreach (var propertyInfo in properties)
            {
                var injectAttribute = propertyInfo.GetCustomAttributeValueOrDefault((InjectAttribute gc) => gc);
                if (injectAttribute != null)
                    yield return
                        new PropertyDependency
                        {
                            Property = propertyInfo,
                            ServiceName = injectAttribute.ServiceName,
                            ServiceType = propertyInfo.PropertyType,
                            IsRequired = true
                        };
            }
        }
    }
}