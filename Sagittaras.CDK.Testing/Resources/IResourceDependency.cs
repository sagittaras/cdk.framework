using Amazon.CDK.Assertions;

namespace Sagittaras.CDK.Testing.Resources;

/// <summary>
/// Helps to describe and resolve dependencies between resources.
/// </summary>
public interface IResourceDependency
{
    /// <summary>
    /// Assigns the given assertion to the dependency.
    /// </summary>
    /// <param name="assertion"></param>
    /// <typeparam name="TResourceAssertion"></typeparam>
    /// <returns></returns>
    IResourceDependency With<TResourceAssertion>(TResourceAssertion assertion)
        where TResourceAssertion : IResourceAssertion;
    
    /// <summary>
    /// Resolves the dependencies from the given template.
    /// </summary>
    /// <param name="template"></param>
    /// <returns></returns>
    IEnumerable<string> Resolve(Template template);
}