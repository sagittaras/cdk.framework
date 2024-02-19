using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;

namespace Sagittaras.CDK.Framework.Amplify.BuildSpecification.Abstraction;

/// <summary>
/// Describes a section that configures frontend of the Amplify application.
/// </summary>
public interface IFrontendSection : IBuildSpecSection
{
    /// <summary>
    /// Describes the phases of the frontend.
    /// </summary>
    IPhasesSection Phases { get; }
    
    /// <summary>
    /// Describes the frontend artifacts.
    /// </summary>
    IArtifactsSection Artifacts { get; }
    
    /// <summary>
    /// Describes the cached frontend resources.
    /// </summary>
    ICacheSection Cache { get; }
}