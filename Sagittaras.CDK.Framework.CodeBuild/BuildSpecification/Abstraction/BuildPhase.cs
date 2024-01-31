namespace Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;

/// <summary>
/// Defines available phase sections in the BuildSpec.
/// </summary>
public enum BuildPhase
{
    Install,
    PreBuild,
    Build,
    PostBuild
}