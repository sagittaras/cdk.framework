using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Amplify.Branch;

/// <summary>
/// Assertion for AWS::Amplify::Branch.
/// </summary>
public class BranchAssertion : ResourceAssertion<BranchProperties>
{
    /// <inheritdoc />
    public override string Type => "AWS::Amplify::Branch";

    /// <summary>
    /// Sets the expected name of the branch.
    /// </summary>
    /// <param name="branchName"></param>
    /// <returns></returns>
    public BranchAssertion WithBranchName(string branchName)
    {
        SetProperty(x => x.BranchName = branchName);
        return this;
    }

    /// <summary>
    /// Sets the expected value of the auto build.
    /// </summary>
    /// <param name="enabled"></param>
    /// <returns></returns>
    public BranchAssertion HasAutoBuildEnabled(bool enabled)
    {
        SetProperty(x => x.EnableAutoBuild = enabled);
        return this;
    }
}