using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.CodeDeploy.DeploymentGroup;

/// <summary>
///     Properties for AWS::CodeDeploy::DeploymentGroup.
/// </summary>
public class DeploymentGroupProperties : ResourceProperties
{
    public string? DeploymentGroupName { get; set; }
    public string? DeploymentConfigName { get; set; }
    public AlarmConfigurationProperties? AlarmConfiguration { get; set; }
}