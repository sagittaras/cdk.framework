using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.KMS;

/// <summary>
/// Properties for AWS::KMS::Alias.
/// </summary>
public class AliasProperties : ResourceProperties
{
    /// <summary>
    /// Name of the key alias.
    /// </summary>
    public string? AliasName { get; set; }
}