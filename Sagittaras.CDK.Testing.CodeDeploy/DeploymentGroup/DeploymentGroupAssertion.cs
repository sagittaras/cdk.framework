using Amazon.CDK.AWS.CodeDeploy;
using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.CodeDeploy.DeploymentGroup;

/// <summary>
///     Assertion for AWS::CodeDeploy::DeploymentGroup.
/// </summary>
public class DeploymentGroupAssertion : ResourceAssertion<DeploymentGroupProperties>
{
    /// <inheritdoc />
    public override string Type => "AWS::CodeDeploy::DeploymentGroup";

    public DeploymentGroupAssertion WithGroupName(string groupName)
    {
        SetProperty(x => x.DeploymentGroupName = groupName);
        return this;
    }

    public DeploymentGroupAssertion WithLambdaDeploymentConfig(ILambdaDeploymentConfig config)
    {
        SetProperty(x => x.DeploymentConfigName = config.DeploymentConfigName);
        return this;
    }

    public DeploymentGroupAssertion HasAlarmsEnabled(bool enabled)
    {
        SetProperty(x =>
        {
            x.AlarmConfiguration ??= new AlarmConfigurationProperties();
            x.AlarmConfiguration.Enabled = enabled;
        });
        return this;
    }
}