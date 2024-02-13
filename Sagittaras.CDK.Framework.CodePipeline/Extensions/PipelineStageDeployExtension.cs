using Sagittaras.CDK.Framework.CodePipeline.Stages;
using Sagittaras.CDK.Framework.CodePipeline.Stages.Deploy;

namespace Sagittaras.CDK.Framework.CodePipeline.Extensions;

/// <summary>
///     Adds useful methods for defining deploy actions on the pipeline stage.
/// </summary>
public static class PipelineStageDeployExtension
{
    /// <summary>
    ///     Shortcut for creating the "Deploy" stage.
    /// </summary>
    /// <param name="factory"></param>
    /// <returns></returns>
    public static PipelineStageBuilder HasDeployStage(this PipelineFactory factory)
    {
        return factory.HasStage("Deploy");
    }

    /// <summary>
    ///     Adds a manual approval action to the stage.
    /// </summary>
    /// <param name="stageBuilder"></param>
    /// <param name="actionName"></param>
    /// <param name="information"></param>
    /// <returns></returns>
    public static PipelineStageBuilder UsesManualApproval(this PipelineStageBuilder stageBuilder, string actionName, string? information = null)
    {
        stageBuilder.AddAction(new ManualApprovalActionBuilder(stageBuilder, actionName, information));
        return stageBuilder;
    }
}