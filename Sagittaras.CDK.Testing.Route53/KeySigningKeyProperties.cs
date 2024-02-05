using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Route53;

/// <summary>
/// Properties for AWS::Route53::KeySigningKey.
/// </summary>
public class KeySigningKeyProperties : ResourceProperties
{
    /// <summary>
    /// Current status of the KSK.
    /// </summary>
    public string? Status { get; set; }
    
    /// <summary>
    /// Name of the KSK.
    /// </summary>
    public string? Name { get; set; }
}