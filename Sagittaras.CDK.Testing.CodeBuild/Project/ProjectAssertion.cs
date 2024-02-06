using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.CodeBuild.Project;

/// <summary>
/// Assertion for AWS::CodeBuild::Project.
/// </summary>
public class ProjectAssertion : ResourceAssertion<ProjectProperties>
{
    /// <inheritdoc />
    public override string Type => "AWS::CodeBuild::Project";
}