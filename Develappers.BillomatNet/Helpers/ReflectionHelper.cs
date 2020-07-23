using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Develappers.BillomatNet.Helpers
{
    internal static class ReflectionHelper
    {
        private static MemberExpression GetMemberExpression(Expression method)
        {
            if (!(method is LambdaExpression lambda))
                throw new ArgumentNullException(nameof(method));

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr =
                    ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
                throw new ArgumentException("method");

            return memberExpr;
        }

        /// <summary>
        /// Gets the attribute of a property.
        /// </summary>
        /// <param name="propertyExpression">The property.</param>
        /// <returns>The property information.</returns>
        public static PropertyInfo GetPropertyInfo(Expression propertyExpression)
        {
            return GetMemberExpression(propertyExpression).Member as PropertyInfo;
        }
    }
}
