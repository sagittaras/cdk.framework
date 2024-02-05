using System.Reflection;

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
    public TProperties Properties { get; set; } = new();

    /// <summary>
    /// Gets the property members of the derived class.
    /// </summary>
    private IEnumerable<PropertyInfo> PropertyMembers => GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

    /// <inheritdoc />
    public IDictionary<string, object> GetResourceDescription()
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
            }

            description.Add(member.Name, value);
        }

        return description;
    }
}