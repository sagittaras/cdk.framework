using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;
using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Factory;
using Xunit;

namespace Sagittaras.CDK.Tests.CodeBuild.BuildSpecification;

/// <summary>
/// Tests creation of build specifications.
/// </summary>
public class BuildSpecFactoryTest
{
    [Fact(Skip = "JSII weird behaviour on host machine.")]
    public void Test_BasicTranslate()
    {
        CodeBuildSpecFactory factory = new();
        factory.Phases.Phase(BuildPhase.Build)
            .Command("echo \"Hello, World!\"");

        string spec = factory.ToBuildSpecYaml().ToBuildSpec();
        Assert.Contains("version: 0.2", spec);
        Assert.Contains("on-failure: CONTINUE", spec);
        Assert.Contains("- echo \"Hello, World!\"", spec);
        Assert.Contains("build:", spec);
        Assert.Contains("commands:", spec);
    }
}