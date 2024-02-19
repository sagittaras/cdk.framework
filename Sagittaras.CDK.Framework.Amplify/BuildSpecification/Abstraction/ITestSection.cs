using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;

namespace Sagittaras.CDK.Framework.Amplify.BuildSpecification.Abstraction;

/// <summary>
/// Describes the section of the BuildSpec file.
/// </summary>
public interface ITestSection : IBuildSpecSection
{
    /// <summary>
    /// Access to the artifacts section.
    /// </summary>
    IArtifactsSection Artifacts { get; }
    
    /// <summary>
    /// Gets the phase this section is describing.
    /// </summary>
    /// <param name="phase"></param>
    /// <returns></returns>
    ITestPhaseSection Phase(TestPhase phase);
}