using Amazon.CDK.AWS.CodePipeline.Actions;
using Sagittaras.CDK.Framework.CodePipeline.Stages;
using Sagittaras.CDK.Framework.CodePipeline.Stages.Source;

namespace Sagittaras.CDK.Framework.CodePipeline.Extensions;

/// <summary>
/// Adds useful methods for defining sources on the pipeline stage.
/// </summary>
public static class PipelineStageSourceExtension
{
    /// <summary>
    /// Shortcut for creating the "Source" stage.
    /// </summary>
    /// <param name="factory"></param>
    /// <returns></returns>
    public static PipelineStageBuilder HasSourceStage(this PipelineFactory factory)
    {
        return factory.HasStage("Source");
    }

    /// <summary>
    /// Adds CodeStar source to the stage.
    /// </summary>
    /// <param name="stageBuilder"></param>
    /// <param name="sourceName"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static CodeStarConnectionsSourceAction UsesCodeStar(this PipelineStageBuilder stageBuilder, string sourceName, Action<CodeStarBuilder> configure)
    {
        CodeStarBuilder builder = new(stageBuilder, sourceName);
        configure.Invoke(builder);

        CodeStarConnectionsSourceAction action = builder.Construct();
        stageBuilder.AddAction(action);

        return action;
    }

    /// <summary>
    /// Adds ECR source to the stage.
    /// </summary>
    /// <param name="stageBuilder"></param>
    /// <param name="sourceName"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static EcrSourceAction UsesEcr(this PipelineStageBuilder stageBuilder, string sourceName, Action<EcrSourceBuilder> configure)
    {
        EcrSourceBuilder builder = new(stageBuilder, sourceName);
        configure.Invoke(builder);

        EcrSourceAction action = builder.Construct();
        stageBuilder.AddAction(action);

        return action;
    }
    
    /// <summary>
    /// Adds S3 source to the stage.
    /// </summary>
    /// <param name="stageBuilder"></param>
    /// <param name="sourceName"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static S3SourceAction UsesS3Source(this PipelineStageBuilder stageBuilder, string sourceName, Action<S3SourceBuilder> configure)
    {
        S3SourceBuilder builder = new(stageBuilder, sourceName);
        configure.Invoke(builder);

        S3SourceAction action = builder.Construct();
        stageBuilder.AddAction(action);

        return action;
    }
}