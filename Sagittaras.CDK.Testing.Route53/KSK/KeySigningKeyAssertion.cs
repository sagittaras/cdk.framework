using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Route53.KSK;

/// <summary>
/// Assertion for AWS::Route53::KeySigningKey.
/// </summary>
public class KeySigningKeyAssertion : ResourceAssertion<KeySigningKeyProperties>
{
    /// <inheritdoc />
    public override string Type => "AWS::Route53::KeySigningKey";
}