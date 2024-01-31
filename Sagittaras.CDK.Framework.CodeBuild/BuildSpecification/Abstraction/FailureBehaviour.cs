namespace Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;

/// <summary>
/// Enum describing possible behaviours when the build fails.
/// </summary>
public enum FailureBehaviour
{
    /// <summary>
    /// Abort the build.
    /// </summary>
    Abort,

    /// <summary>
    /// Continue to the next phase.
    /// </summary>
    Continue
}