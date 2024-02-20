using Amazon.CDK.AWS.CodePipeline.Actions;

namespace Sagittaras.CDK.Framework.CodePipeline.Stages.Deploy;

/// <summary>
///     Creates a action that is responsible for executing the change set on the CloudFormation stack.
/// </summary>
public class CloudFormationExecuteChangeSetActionBuilder : ActionBuilder<CloudFormationExecuteChangeSetAction>
{
    private readonly CloudFormationExecuteChangeSetActionProps _props;

    public CloudFormationExecuteChangeSetActionBuilder(PipelineStageBuilder builder, string name) : base(builder, name)
    {
        _props = new CloudFormationExecuteChangeSetActionProps
        {
            ActionName = name
        };
    }

    /// <inheritdoc />
    public override CloudFormationExecuteChangeSetAction Construct()
    {
        return new CloudFormationExecuteChangeSetAction(_props);
    }

    /// <inheritdoc />
    public override IActionBuilder RunOrder(int order)
    {
        _props.RunOrder = order;
        return this;
    }

    /// <summary>
    /// </summary>
    /// <param name="changeSetName"></param>
    /// <returns></returns>
    public CloudFormationExecuteChangeSetActionBuilder ForChangeSet(string changeSetName)
    {
        _props.ChangeSetName = changeSetName;
        return this;
    }

    /// <summary>
    /// </summary>
    /// <param name="stackName"></param>
    /// <returns></returns>
    public CloudFormationExecuteChangeSetActionBuilder WithStackName(string stackName)
    {
        _props.StackName = stackName;
        return this;
    }
}