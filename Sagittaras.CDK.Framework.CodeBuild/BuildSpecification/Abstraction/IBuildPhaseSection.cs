namespace Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;

/// <summary>
/// Describes a phase section in the build spec file.
/// </summary>
public interface IBuildPhaseSection : IBuildSpecSection
{
    /// <summary>
    /// Configures the behaviour on failure of the phase.
    /// </summary>
    /// <param name="failureBehaviour"></param>
    /// <returns></returns>
    IBuildPhaseSection OnFailure(FailureBehaviour failureBehaviour);

    /// <summary>
    /// Define a new command for the phase.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    IBuildPhaseSection Command(string command);

    /// <summary>
    /// Adds a command to the finally section, which is executed regardless of the phase result.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    IBuildPhaseSection Finally(string command);
}