using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.CodeDeploy.Application;

/// <summary>
///     Properties for AWS::CodeDeploy::Application.
/// </summary>
public class ApplicationProperties : ResourceProperties
{
    public string? ApplicationName { get; set; }
    public ComputePlatform? ComputePlatform { get; set; }
}