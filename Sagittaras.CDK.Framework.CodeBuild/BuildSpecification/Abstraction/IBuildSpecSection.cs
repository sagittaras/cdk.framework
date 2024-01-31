namespace Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;

/// <summary>
/// Define a section in the build spec factory, that contains custom configuration options.
/// </summary>
public interface IBuildSpecSection
{
    /// <summary>
    /// Gets the name of the section.
    /// </summary>
    string SectionName { get; }

    /// <summary>
    /// Converts the section to the dictionary object suitable for BuildSpec serialization in CDK.
    /// </summary>
    /// <returns></returns>
    IDictionary<string, object> ToDictionary();
}