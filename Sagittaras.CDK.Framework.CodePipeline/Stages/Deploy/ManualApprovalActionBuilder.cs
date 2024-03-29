using Amazon.CDK.AWS.CodePipeline.Actions;

namespace Sagittaras.CDK.Framework.CodePipeline.Stages.Deploy;

public class ManualApprovalActionBuilder : ActionBuilder<ManualApprovalAction>
{
    private readonly ManualApprovalActionProps _props;

    public ManualApprovalActionBuilder(PipelineStageBuilder scope, string name, string? information = null) : base(scope, name)
    {
        _props = new ManualApprovalActionProps
        {
            ActionName = name,
            AdditionalInformation = information
        };
    }

    /// <inheritdoc />
    public override ManualApprovalAction Construct()
    {
        return new ManualApprovalAction(_props);
    }

    /// <inheritdoc />
    public override IActionBuilder RunOrder(int order)
    {
        _props.RunOrder = order;
        return this;
    }

    public ManualApprovalActionBuilder WithInformation(string information)
    {
        _props.AdditionalInformation = information;
        return this;
    }
}