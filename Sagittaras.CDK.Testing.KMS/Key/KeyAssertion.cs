using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.KMS.Key;

/// <summary>
/// Assertion for AWS::KMS::Key.
/// </summary>
public class KeyAssertion : ResourceAssertion<KeyProperties>
{
    public override string Type => "AWS::KMS::Key";
}