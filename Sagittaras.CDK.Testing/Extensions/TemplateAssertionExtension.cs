using Amazon.CDK.Assertions;
using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Extensions;

/// <summary>
/// Extends the assertion template from the CDK by custom assertion methods.
/// </summary>
public static class TemplateAssertionExtension
{
    /// <summary>
    /// Asserts that the template has a resource with the given description.
    /// </summary>
    /// <param name="template"></param>
    /// <param name="assertion"></param>
    /// <typeparam name="TResourceAssertion"></typeparam>
    public static void Assert<TResourceAssertion>(this Template template, TResourceAssertion assertion)
        where TResourceAssertion : IResourceAssertion
    {
        template.HasResource(assertion.Type, assertion.GetResourceDescription(template));
    }

    /// <summary>
    /// Asserts that the template has a given number of resources of the given type.
    /// </summary>
    /// <param name="template"></param>
    /// <param name="count"></param>
    /// <typeparam name="TResourceAssertion"></typeparam>
    public static void AssertCount<TResourceAssertion>(this Template template, int count)
        where TResourceAssertion : IResourceAssertion, new()
    {
        template.ResourceCountIs(new TResourceAssertion().Type, count);
    }
}