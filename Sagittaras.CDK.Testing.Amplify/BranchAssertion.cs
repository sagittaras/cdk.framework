using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Amplify;

/// <summary>
/// Assertion for AWS::Amplify::Branch.
/// </summary>
public class BranchAssertion : ResourceAssertion<BranchProperties>
{
    /// <inheritdoc />
    public override string Type => "AWS::Amplify::Branch";
}