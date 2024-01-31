using Amazon.CDK.AWS.CodeBuild;

namespace Sagittaras.CDK.Framework.Amplify.Buildspecs;

/// <summary>
/// Describes a class that can create a build specification.
/// </summary>
public interface IAmplifyBuildspecFactory
{
    /// <summary>
    /// Creates a object describing the build specification.
    /// </summary>
    /// <returns></returns>
    BuildSpec ToBuildSpec();
}