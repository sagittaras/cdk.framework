using Amazon.CDK.AWS.CodeDeploy;
using Constructs;

namespace Sagittaras.CDK.Framework.CodeDeploy.Deployments;

/// <summary>
///     Factory for creating a deployment group for Lambda function.
/// </summary>
public class LambdaDeploymentGroupFactory : DeploymentGroupFactory<LambdaDeploymentGroup, LambdaDeploymentGroupProps>
{
    public LambdaDeploymentGroupFactory(Construct scope, string groupName) : base(scope, groupName)
    {
    }

    /// <inheritdoc />
    public override LambdaDeploymentGroupProps Props { get; } = new();

    /// <inheritdoc />
    public override LambdaDeploymentGroup Construct()
    {
        return new LambdaDeploymentGroup(this, "deployment-group", Props);
    }
}