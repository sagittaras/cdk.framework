using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.CodeBuild.Project;

public class ProjectProperties : ResourceProperties
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public EnvironmentProperties? Environment { get; set; }
}