using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.CodeBuild.Project;

/// <summary>
/// Assertion for AWS::CodeBuild::Project.
/// </summary>
public class ProjectAssertion : ResourceAssertion<ProjectProperties>
{
    /// <inheritdoc />
    public override string Type => "AWS::CodeBuild::Project";

    private void SetEnvironmentProperty(Action<EnvironmentProperties> action)
    {
        SetProperty(x =>
        {
            x.Environment ??= new EnvironmentProperties();
            action(x.Environment);
        });
    }

    public ProjectAssertion WithProjectName(string projectName)
    {
        SetProperty(x => x.Name = projectName);
        return this;
    }

    public ProjectAssertion HasComputeType(ComputeType type)
    {
        SetEnvironmentProperty(x => x.ComputeType = type);
        return this;
    }

    public ProjectAssertion HasEnvironmentType(EnvironmentType type)
    {
        SetEnvironmentProperty(x => x.Type = type);
        return this;
    }

    public ProjectAssertion UsesImage(string image)
    {
        SetEnvironmentProperty(x => x.Image = image);
        return this;
    }
}