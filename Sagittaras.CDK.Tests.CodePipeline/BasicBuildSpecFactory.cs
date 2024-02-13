using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;
using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Factory;

namespace Sagittaras.CDK.Tests.CodePipeline;

/// <summary>
///     The most basic build spec which can be used for testing purposes.
/// </summary>
public class BasicBuildSpecFactory : BuildSpecFactory
{
    public BasicBuildSpecFactory()
    {
        Phases.BuildPhase(BuildPhase.Build)
            .Command("echo \"Hello, World!\"")
            ;
    }
}