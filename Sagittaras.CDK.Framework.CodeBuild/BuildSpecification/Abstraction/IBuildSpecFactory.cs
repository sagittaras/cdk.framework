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
    /// Gets an environment section of the BuildSpec for further definition.
    /// </summary>
    /// <returns></returns>
    IEnvironmentSection Environment { get; }

    /// <summary>
    /// Gets a section describing available phases of the BuildSpec for further definition.
    /// </summary>
    IPhasesSection Phases { get; }

    /// <summary>
    /// Gets an artifacts section of the BuildSpec for further definition.
    /// </summary>
    /// <returns></returns>
    IArtifactsSection Artifacts { get; }

    /// <summary>
    /// Gets an cache section of the BuildSpec for further definition.
    /// </summary>
    /// <returns></returns>
    ICacheSection Cache { get; }

    /// <summary>
    /// Specifies the version of the buildspec.
    /// </summary>
    IBuildSpecFactory Version(double version);

    /// <summary>
    /// Converts the factory to a BuildSpec object.
    /// </summary>
    /// <returns></returns>
    BuildSpec ToBuildSpec();

    /// <summary>
    /// Helps to determine additional policies for the project, required
    /// by the BuildSpec.
    /// </summary>
    /// <param name="project"></param>
    void ConfigureProjectPolicies(IProject project);
}