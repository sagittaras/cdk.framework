using Amazon.CDK.AWS.CodeBuild;
using Sagittaras.CDK.Framework;
using Sagittaras.CDK.Framework.CodeBuild;
using Sagittaras.CDK.Framework.CodePipeline;
using Sagittaras.CDK.Framework.CodePipeline.Extensions;
using Sagittaras.CDK.Framework.CodePipeline.Stages;
using Sagittaras.CDK.Testing.CodePipeline.Pipeline;
using Sagittaras.CDK.Testing.CodePipeline.Pipeline.ArtifactStore;
using Xunit;

namespace Sagittaras.CDK.Tests.CodePipeline;

public class PipelineFactoryTest : ConstructTest
{
    private const string PipelineName = "TestPipeline";
    private const string ArtifactBucket = "artifacts";
    private const string SourceCodeArtifact = "source-code";

    /// <summary>
    ///     The most basic construct for the pipeline.
    /// </summary>
    [Fact]
    public void Test_BaseConstruct()
    {
        PipelineFactory factory = new(Stack, PipelineName, ArtifactBucket);

        PipelineStageBuilder source = factory.HasSourceStage();
        source.UsesCodeStar("GitHub")
            .UsesConnection("arn:aws:codestar-connections:eu-west-1:123456789012:connection/12345678-1234-1234-1234-123456789012")
            .FromRepository("sagittaras", "cdk.framework")
            .UseBranch("main")
            .HasOutput(SourceCodeArtifact)
            ;

        PipelineStageBuilder build = factory.HasBuildStage();
        build.UsesCodeBuild("Build")
            .UsesProject(ConstructCodeBuildProject())
            .UsesInputArtifact(SourceCodeArtifact)
            ;

        _ = factory.GetStageAction("Source", "GitHub");
        
        factory.Construct();

        new PipelineAssertion()
            .SetProperty(x => x.Name, Cloudspace.ResourceName(PipelineName))
            .PropertyGroup(x => x.ArtifactStore!, x =>
            {
                x.Type = ArtifactStoreType.S3;
                x.Location = ArtifactBucket;
            })
            .Assert(StackTemplate);
    }

    /// <summary>
    ///     Creates a new code build project for the pipeline.
    /// </summary>
    /// <returns></returns>
    private IProject ConstructCodeBuildProject()
    {
        return new CodeBuildFactory(Stack, "BuildProject")
            .UsesBuildSpec<BasicBuildSpecFactory>()
            .Construct();
    }
}