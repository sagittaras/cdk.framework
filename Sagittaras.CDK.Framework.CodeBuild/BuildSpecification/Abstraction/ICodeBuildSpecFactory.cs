using Amazon.CDK.AWS.CodeBuild;

namespace Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;

/// <summary>
/// Describes a factory that is specific for Code Build module.
/// </summary>
public interface ICodeBuildSpecFactory : IBuildSpecFactory
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
    /// Helps to determine additional policies for the project, required
    /// by the BuildSpec.
    /// </summary>
    /// <param name="project"></param>
    void ConfigureProjectPolicies(IProject project);
}