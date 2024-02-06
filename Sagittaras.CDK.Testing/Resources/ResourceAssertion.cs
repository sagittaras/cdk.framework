using System.Reflection;
using Amazon.CDK.Assertions;

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
}