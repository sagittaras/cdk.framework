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
    /// <returns></returns>
    public static CodeStarBuilder UsesCodeStar(this PipelineStageBuilder stageBuilder, string sourceName)
    {
        CodeStarBuilder builder = new(stageBuilder, sourceName);
        stageBuilder.AddAction(builder);

        return builder;
    }

    /// <summary>
    /// Adds ECR source to the stage.
    /// </summary>
    /// <param name="stageBuilder"></param>
    /// <param name="sourceName"></param>
    /// <returns></returns>
    public static EcrSourceBuilder UsesEcr(this PipelineStageBuilder stageBuilder, string sourceName)
    {
        EcrSourceBuilder builder = new(stageBuilder, sourceName);
        stageBuilder.AddAction(builder);

        return builder;
    }
}