namespace Sagittaras.CDK.Testing.Resources;

/// <summary>
/// Describes the properties of a resource.
/// </summary>
public interface IResourceProperties
{
    /// <summary>
    /// Converts the properties to a dictionary suitable for template assertion.
    /// </summary>
    /// <returns></returns>
    IDictionary<string, object> ToDictionary();
}