using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Lambda.Function;

/// <summary>
///     Properties for AWS::Lambda::Function.
/// </summary>
public class FunctionProperties : ResourceProperties
{
    public string? FunctionName { get; set; }
    public int? MemorySize { get; set; }
    public int? Timeout { get; set; }
    public TracingConfigProperties? TracingConfig { get; set; }
    public PackageType? PackageType { get; set; }
    public EnvironmentProperties? Environment { get; set; }
}