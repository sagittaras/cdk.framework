using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.KMS.Alias;

/// <summary>
/// Assertion for AWS::KMS::Alias.
/// </summary>
public class AliasAssertion : ResourceAssertion<AliasProperties>
{
    public override string Type => "AWS::KMS::Alias";

    public AliasAssertion WithAliasName(string aliasName)
    {
        SetProperty(x => x.AliasName = aliasName);
        return this;
    }
}