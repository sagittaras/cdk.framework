using System.Collections;
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
            switch (value)
            {
                case null:
                    continue;
                case IResourceProperties resourceProperties:
                    dict[property.Name] = resourceProperties.ToDictionary();
                    break;
                case IEnumerable<IResourceProperties> propsCollection:
                    // Without cast, it's causing a JSII error, because in reflection is marked as interface.
                    dict[property.Name] = propsCollection.Select(x => x.ToDictionary() as Dictionary<string, object>).ToArray();
                    break;
                default:
                    dict[property.Name] = value;
                    break;
            }
        }

        return dict;
    }
}