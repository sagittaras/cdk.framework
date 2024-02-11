using System.Linq.Expressions;

namespace Sagittaras.CDK.Framework.Props;

/// <summary>
/// </summary>
public static class ExpressionMapper
{
    /// <summary>
    ///     Sets the value of the property from the expression.
    /// </summary>
    /// <param name="expression">Expression which returns the member of the source.</param>
    /// <param name="instance">Instance of the object on which the property is set.</param>
    /// <param name="value">Value to be set.</param>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <exception cref="ArgumentException"></exception>
    public static void SetValue<TSource, TProperty>(Expression<Func<TSource, TProperty>> expression, object instance, object value)
    {
        if (expression.Body is MemberExpression member)
        {
            typeof(TSource)
                .GetProperty(member.Member.Name)?
                .SetValue(instance, value);
        }
        else
        {
            throw new ArgumentException($"Argument must be {nameof(MemberExpression)}", nameof(expression));
        }
    }
}