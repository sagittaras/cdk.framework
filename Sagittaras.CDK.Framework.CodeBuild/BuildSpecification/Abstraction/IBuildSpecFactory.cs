using Amazon.CDK.AWS.CodeBuild;

namespace Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;

/// <summary>
/// Describes a factory that helps to configure different sections of the BuildSpec for
/// CodeBuild and returns the final BuildSpec.
/// </summary>
/// <remarks>
/// API of the factory allows easily extendable and overridable configuration of the BuildSpec,
/// which brings better comfort than defining JSON or YAML files for the BuildSpec.
/// </remarks>
public interface IBuildSpecFactory
{
    /// <summary>
    /// Specifies the version of the buildspec.
    /// </summary>
    IBuildSpecFactory Version(double version);

    /// <summary>
    /// Converts the factory to a BuildSpec object using YAML.
    /// </summary>
    /// <returns></returns>
    BuildSpec ToBuildSpecYaml();

    /// <summary>
    /// Converts the factory to a BuildSpec object using JSON.
    /// </summary>
    /// <returns></returns>
    BuildSpec ToBuildSpecJson();
}