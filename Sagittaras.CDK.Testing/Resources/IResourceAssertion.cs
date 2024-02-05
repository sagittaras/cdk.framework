using Amazon.CDK.Assertions;

namespace Sagittaras.CDK.Testing.Resources;

/// <summary>
/// Basic interface describing the assertion for AWS resource.
/// </summary>
public interface IResourceAssertion
{
    /// <summary>
    /// AWS Resource type.
    /// </summary>
    string Type { get; }

    /// <summary>
    /// Maps on which resources the current resource depends on.
    /// </summary>
    IResourceDependency? DependsOn { get; set; }

    /// <summary>
    /// Converts the resource description to a dictionary suitable for template assertion.
    /// </summary>
    /// <returns></returns>
    IDictionary<string, object> GetResourceDescription(Template template);

    /// <summary>
    /// Executes the assertion against the target template.
    /// </summary>
    /// <param name="template">Instance of template.</param>
    void Assert(Template template);
}

/// <summary>
/// Extends the basic resource assertion with properties.
/// </summary>
/// <typeparam name="TProperties">Type of the properties used for the resource.</typeparam>
public interface IResourceAssertion<TProperties> : IResourceAssertion
    where TProperties : IResourceProperties, new()
{
    /// <summary>
    /// Properties that helps to identify the resource.
    /// </summary>
    TProperties? Properties { get; set; }
}