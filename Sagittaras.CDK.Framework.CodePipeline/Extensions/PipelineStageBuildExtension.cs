using Sagittaras.CDK.Framework.CodePipeline.Stages;
using Sagittaras.CDK.Framework.CodePipeline.Stages.Build;

namespace Sagittaras.CDK.Framework.CodePipeline.Extensions;

/// <summary>
/// Adds useful methods for defining build actions on the stage.
/// </summary>
public static class PipelineStageBuildExtension
{
    /// <summary>
    /// Shortcut for creating the "Build" stage.
    /// </summary>
    /// <param name="factory"></param>
    /// <returns></returns>
    public static PipelineStageBuilder HasBuildStage(this PipelineFactory factory)
    {
        return factory.HasStage("Build");
    }

    /// <summary>
    /// Adds CodeBuild action to the stage.
    /// </summary>
    /// <param name="stageBuilder"></param>
    /// <param name="actionName"></param>
    /// <returns></returns>
    public static CodeBuildActionBuilder UsesCodeBuild(this PipelineStageBuilder stageBuilder, string actionName)
    {
        CodeBuildActionBuilder builder = new(stageBuilder, actionName);
        stageBuilder.AddAction(builder);

        return builder;
    }
}