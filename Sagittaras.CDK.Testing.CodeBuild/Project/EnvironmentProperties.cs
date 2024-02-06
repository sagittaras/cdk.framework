using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.CodeBuild.Project;

public class EnvironmentProperties : ResourceProperties
{
    public ComputeType? ComputeType { get; set; }
    public string? Image { get; set; }
    public EnvironmentType? Type { get; set; }
    public bool? PrivilegedMode { get; set; }
}