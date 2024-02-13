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

    /// <summary>
    ///     Adds a deploy action that will prepare the change set for the CloudFormation stack deployment.
    /// </summary>
    /// <param name="stageBuilder"></param>
    /// <param name="actionName"></param>
    /// <returns></returns>
    public static CloudFormationChangeSetActionBuilder UsesCloudFormationChangeSet(this PipelineStageBuilder stageBuilder, string actionName)
    {
        CloudFormationChangeSetActionBuilder builder = new(stageBuilder, actionName);
        stageBuilder.AddAction(builder);

        return builder;
    }

    /// <summary>
    ///     Adds a deploy action that will execute the change set on the CloudFormation stack.
    /// </summary>
    /// <param name="stageBuilder"></param>
    /// <param name="actionName"></param>
    /// <returns></returns>
    public static CloudFormationExecuteChangeSetActionBuilder UsesCloudFormationExecuteChangeSet(this PipelineStageBuilder stageBuilder, string actionName)
    {
        CloudFormationExecuteChangeSetActionBuilder builder = new(stageBuilder, actionName);
        stageBuilder.AddAction(builder);

        return builder;
    }
}