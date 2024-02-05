using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Amplify.Branch;

/// <summary>
/// Properties of AWS::Amplify::Branch.
/// </summary>
public class BranchProperties : ResourceProperties
{
    public string? BranchName { get; set; }
    public bool? EnableAutoBuild { get; set; }
    public bool? EnablePullRequestPreview { get; set; }
}