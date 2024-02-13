using System.Linq.Expressions;
using System.Reflection;
using Amazon.CDK.Assertions;
using Sagittaras.CDK.Framework.Props;

namespace Sagittaras.CDK.Testing.Resources;

/// <summary>
/// Abstract implementation of <see cref="IResourceAssertion{TProperties}"/>.
/// </summary>
public abstract class ResourceAssertion<TProperties> : IResourceAssertion<TProperties>
    where TProperties : IResourceProperties, new()
{
    /// <inheritdoc />
    public abstract string Type { get; }

    /// <inheritdoc />
    public IResourceDependency? DependsOn { get; set; }

    /// <inheritdoc />
    public TProperties? Properties { get; set; }

    /// <summary>
    /// Gets the property members of the derived class.
    /// </summary>
    private IEnumerable<PropertyInfo> PropertyMembers => GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

    /// <inheritdoc />
    public IDictionary<string, object> GetResourceDescription(Template template)
    {
        Dictionary<string, object> description = new();
        foreach (PropertyInfo member in PropertyMembers)
        {
            object? value = member.GetValue(this);
            switch (value)
            {
                case null:
                    continue;
                case IResourceProperties properties:
                    value = properties.ToDictionary();
                    break;
                case IResourceDependency dependency:
                    value = dependency.Resolve(template);
                    break;
            }

            description.Add(member.Name, value);
        }

        return description;
    }

    /// <inheritdoc />
    public void Assert(Template template)
    {
        template.HasResource(Type, GetResourceDescription(template));
    }

    /// <inheritdoc />
    public void AssertCount(Template template, int count)
    {
        template.ResourceCountIs(Type, count);
    }

    /// <summary>
    /// Access the existing resource properties.
    /// </summary>
    /// <remarks>
    /// If properties are not available, create a new instance.
    /// </remarks>
    /// <returns></returns>
    private TProperties GetResourceProperties()
    {
        if (Properties is null)
        {
            Properties = new TProperties();
        }

        return Properties;
    }

    /// <summary>
    /// Sets the value of the property by provided action which ensures existing props instance.
    /// </summary>
    /// <param name="props"></param>
    protected void SetProperty(Action<TProperties> props)
    {
        props(GetResourceProperties());
    }

    /// <summary>
    ///     Sets the expected value of the property.
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="value"></param>
    /// <typeparam name="TProperty"></typeparam>
    public ResourceAssertion<TProperties> SetProperty<TProperty>(Expression<Func<TProperties, TProperty>> expression, TProperty value)
    {
        Properties ??= new TProperties();
        ExpressionMapper.SetValue(expression, Properties, value);
        return this;
    }

    /// <summary>
    ///     Initiates a callback to configure properties of the property group.
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="configure"></param>
    /// <typeparam name="TProperty"></typeparam>
    /// <returns></returns>
    public ResourceAssertion<TProperties> PropertyGroup<TProperty>(Expression<Func<TProperties, TProperty>> expression, Action<TProperty> configure)
        where TProperty : class, IResourceProperties, new()
    {
        Properties ??= new TProperties();
        TProperty group = GetPropertyGroup(expression, Properties);
        configure.Invoke(group);

        ExpressionMapper.SetValue(expression, Properties, group);

        return this;
    }

    /// <summary>
    ///     Gets an instance of the property group or create a new one.
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="instance"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TPropertyGroup"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private static TPropertyGroup GetPropertyGroup<TSource, TPropertyGroup>(Expression<Func<TSource, TPropertyGroup>> expression, object instance)
        where TPropertyGroup : class, IResourceProperties, new()
    {
        if (expression.Body is not MemberExpression member)
        {
            throw new ArgumentException($"Argument must be {nameof(MemberExpression)}", nameof(expression));
        }

        TPropertyGroup? group = (TPropertyGroup?)typeof(TSource)
            .GetProperty(member.Member.Name)?
            .GetValue(instance);

        return group ?? new TPropertyGroup();
    }
}