using Amazon.CDK.AWS.CodeBuild;
using Sagittaras.CDK.Framework;
using Sagittaras.CDK.Framework.CodeBuild;
using Sagittaras.CDK.Framework.CodePipeline;
using Sagittaras.CDK.Framework.CodePipeline.Extensions;
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

        factory.HasSourceStage()
            .UsesCodeStar("GitHub", x =>
            {
                x.UsesConnection("arn")
                    .FromRepository("sagittaras", "cdk.framework")
                    .UseBranch("main")
                    .HasOutput(SourceCodeArtifact)
                    ;
            });

        factory.HasBuildStage()
            .UsesCodeBuild("Build", x =>
            {
                IProject project = new CodeBuildFactory(Stack, "BuildProject")
                    .UsesBuildSpec<BasicBuildSpecFactory>()
                    .Construct();

                x.UsesProject(project)
                    .UsesInputArtifact(SourceCodeArtifact)
                    ;
            });

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
}