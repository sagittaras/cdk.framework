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
    /// <param name="builder"></param>
    /// <param name="sourceName"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static PipelineStageBuilder UsesCodeStar(this PipelineStageBuilder builder, string sourceName, Action<CodeStarFactory> configure)
    {
        CodeStarFactory factory = new(builder, sourceName);
        configure.Invoke(factory);
        builder.AddAction(factory.Construct());

        return builder;
    }
}