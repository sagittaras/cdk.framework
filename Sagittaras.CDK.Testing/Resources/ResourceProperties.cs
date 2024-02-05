using System.Reflection;

namespace Sagittaras.CDK.Testing.Resources;

/// <summary>
/// Abstract implementation of <see cref="IResourceProperties"/> that provides a default implementation of <see cref="IResourceProperties.ToDictionary"/>.
/// </summary>
/// <remarks>
/// The resource properties should be nullable types to exclude them from the dictionary.
/// </remarks>
public abstract class ResourceProperties : IResourceProperties
{
    /// <summary>
    /// Gets all properties members of the class.
    /// </summary>
    private IEnumerable<PropertyInfo> Properties => GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

    /// <inheritdoc />
    public IDictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> dict = new();
        foreach (PropertyInfo property in Properties)
        {
            object? value = property.GetValue(this);
            if (value is null)
            {
                continue;
            }

            dict[property.Name] = value;
        }

        return dict;
    }
}