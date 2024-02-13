using Amazon.CDK.AWS.CodePipeline.Actions;
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
    /// <param name="configure"></param>
    /// <returns></returns>
    public static CodeBuildAction UsesCodeBuild(this PipelineStageBuilder stageBuilder, string actionName, Action<CodeBuildActionBuilder> configure)
    {
        CodeBuildActionBuilder builder = new(stageBuilder, actionName);
        configure.Invoke(builder);

        CodeBuildAction action = builder.Construct();
        stageBuilder.AddAction(action);

        return action;
    }
}