using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;

namespace Sagittaras.CDK.Framework.Amplify.BuildSpecification.Abstraction;

public interface IAmplifyBuildSpecFactory : IBuildSpecFactory
{
    /// <summary>
    /// Access to the environment section of the build spec.
    /// </summary>
    IEnvironmentSection Environment { get; }
    
    /// <summary>
    /// Access to the frontend configuration.
    /// </summary>
    IFrontendSection Frontend { get; }
}