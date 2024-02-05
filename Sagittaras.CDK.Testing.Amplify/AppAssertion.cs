using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Amplify;

/// <summary>
/// Assertion for AWS::Amplify::App.
/// </summary>
public class AppAssertion : ResourceAssertion<AppProperties>
{
    /// <inheritdoc />
    public override string Type => "AWS::Amplify::App";
}