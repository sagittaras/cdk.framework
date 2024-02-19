namespace Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;

/// <summary>
/// Describes the phase section of the BuildSpec file.
/// </summary>
public interface IPhasesSection : IBuildSpecSection
{
    /// <summary>
    /// Gets the phase this section is describing.
    /// </summary>
    /// <param name="phase"></param>
    /// <returns></returns>
    IBuildPhaseSection Phase(BuildPhase phase);
}