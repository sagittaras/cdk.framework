using Amazon.CDK.Assertions;
using Sagittaras.CDK.Framework;
using Sagittaras.CDK.Framework.CodeBuild;
using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;
using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Factory;
using Sagittaras.CDK.Testing.CodeBuild.Project;
using Xunit;
using ComputeType = Sagittaras.CDK.Testing.CodeBuild.Project.ComputeType;

namespace Sagittaras.CDK.Tests.CodeBuild;

public class ProjectTest : ConstructTest
{
    /// <summary>
    /// Default name for the code build projects.
    /// </summary>
    private const string ProjectName = "TestProject";

    /// <summary>
    /// Default buildspec for the project.
    /// </summary>
    private CodeBuildSpecFactory DefaultSpec
    {
        get
        {
            CodeBuildSpecFactory factory = new();
            factory.Phases.Phase(BuildPhase.Build)
                .Command("echo \"Hello world!\"");

            return factory;
        }
    }

    /// <summary>
    /// Tests basic creation of the CodeBuild project.
    /// </summary>
    [Fact]
    public void Test_BaseConstruct()
    {
        new CodeBuildFactory(Stack, ProjectName)
            .UsesBuildSpec(DefaultSpec)
            .Construct();

        Template template = StackTemplate;

        new ProjectAssertion()
            .AssertCount(template, 1);

        new ProjectAssertion()
            .WithProjectName(Cloudspace.ResourceName(ProjectName))
            .HasComputeType(ComputeType.General1Small)
            .HasEnvironmentType(EnvironmentType.LinuxContainer)
            .UsesImage("aws/codebuild/amazonlinux2-x86_64-standard:5.0")
            .Assert(template);
    }
}