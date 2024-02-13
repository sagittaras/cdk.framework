using Amazon.CDK.AWS.CodePipeline.Actions;
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
    /// <param name="configure"></param>
    /// <returns></returns>
    public static PipelineStageBuilder UsesManualApproval(this PipelineStageBuilder stageBuilder, string actionName, Action<ManualApprovalActionBuilder> configure)
    {
        ManualApprovalActionBuilder builder = new(stageBuilder, actionName);
        configure.Invoke(builder);

        ManualApprovalAction action = builder.Construct();
        stageBuilder.AddAction(action);

        return stageBuilder;
    }

    /// <summary>
    ///     Adds a deploy action that will prepare the change set for the CloudFormation stack deployment.
    /// </summary>
    /// <param name="stageBuilder"></param>
    /// <param name="actionName"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static CloudFormationCreateReplaceChangeSetAction UsesCloudFormationChangeSet(this PipelineStageBuilder stageBuilder, string actionName, Action<CloudFormationChangeSetActionBuilder> configure)
    {
        CloudFormationChangeSetActionBuilder builder = new(stageBuilder, actionName);
        configure.Invoke(builder);

        CloudFormationCreateReplaceChangeSetAction action = builder.Construct();
        stageBuilder.AddAction(action);

        return action;
    }

    /// <summary>
    ///     Adds a deploy action that will execute the change set on the CloudFormation stack.
    /// </summary>
    /// <param name="stageBuilder"></param>
    /// <param name="actionName"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static CloudFormationExecuteChangeSetAction UsesCloudFormationExecuteChangeSet(this PipelineStageBuilder stageBuilder, string actionName, Action<CloudFormationExecuteChangeSetActionBuilder> configure)
    {
        CloudFormationExecuteChangeSetActionBuilder builder = new(stageBuilder, actionName);
        configure.Invoke(builder);

        CloudFormationExecuteChangeSetAction action = builder.Construct();
        stageBuilder.AddAction(action);

        return action;
    }
}