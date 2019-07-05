using System;
using System.Linq;
using System.Reflection;

namespace GF.DillyDally.Shared.Extensions
{
    public static class AttributeExtensions
    {
        #region - Methoden oeffentlich -

        public static TValue GetCustomAttributeValueOrDefault<TAttribute, TValue>(
            this ICustomAttributeProvider attProvider,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            return GetCustomAttributeValueOrDefault(attProvider, valueSelector, default(TValue));
        }

        public static TValue GetCustomAttributeValueOrDefault<TAttribute, TValue>(
            this ICustomAttributeProvider attProvider,
            Func<TAttribute, TValue> valueSelector, TValue defaultValue)
            where TAttribute : Attribute
        {
            var att = attProvider.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() as TAttribute;
            if (att != null)
            {
                return valueSelector(att);
            }
            return defaultValue;
        }

        #endregion
    }
}