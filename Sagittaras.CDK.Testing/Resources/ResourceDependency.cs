using Amazon.CDK.Assertions;

namespace Sagittaras.CDK.Testing.Resources;

/// <summary>
/// Base implementation of the resource dependency.
/// </summary>
public class ResourceDependency : IResourceDependency
{
    /// <summary>
    /// List of assertions that needs to be resolved as dependencies.
    /// </summary>
    private readonly List<IResourceAssertion> _dependencies = new();

    /// <inheritdoc />
    public IResourceDependency With<TResourceAssertion>(TResourceAssertion assertion) where TResourceAssertion : IResourceAssertion
    {
        _dependencies.Add(assertion);
        return this;
    }

    /// <inheritdoc />
    public IEnumerable<string> Resolve(Template template)
    {
        List<string> resolved = new();
        foreach (IResourceAssertion assertion in _dependencies)
        {
            IEnumerable<string> resolvedIds = template.FindResources(assertion.Type, assertion.GetResourceDescription(template))
                .Select(x => x.Key);

            resolved.AddRange(resolvedIds);
        }

        return resolved.ToArray();
    }
}