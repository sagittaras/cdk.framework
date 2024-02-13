using Amazon.CDK.AWS.CodePipeline.Actions;
using Constructs;

namespace Sagittaras.CDK.Framework.CodePipeline.Stages.Deploy;

public class ManualApprovalActionBuilder : ActionBuilder<ManualApprovalAction>
{
    private readonly ManualApprovalActionProps _props;

    public ManualApprovalActionBuilder(Construct scope, string name, string? information = null) : base(scope, name)
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
}