using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.KMS;

/// <summary>
/// Assertion for AWS::KMS::Alias.
/// </summary>
public class AliasAssertion : ResourceAssertion<AliasProperties>
{
    public override string Type => "AWS::KMS::Alias";
}