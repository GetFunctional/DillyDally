using System;
using System.Linq.Expressions;
using System.Reflection;

namespace GF.DillyDally.Mvvmc
{
    internal static class ObservableObjectExtensions
    {
        internal static string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            return GetPropertyNameFast(expression);
        }

        private static string GetPropertyNameFast(LambdaExpression expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("MemberExpression is expected in expression.Body", "expression");
            }

            const string vblocalPrefix = "$VB$Local_";
            var member = memberExpression.Member;
            if (
                member.MemberType == MemberTypes.Field &&
                member.Name != null &&
                member.Name.StartsWith(vblocalPrefix))
            {
                return member.Name.Substring(vblocalPrefix.Length);
            }

            return member.Name;
        }
    }
}