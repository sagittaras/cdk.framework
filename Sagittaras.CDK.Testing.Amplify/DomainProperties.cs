using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Amplify;

/// <summary>
/// Properties for AWS::Amplify::Domain.
/// </summary>
public class DomainProperties : ResourceProperties
{
    public string? DomainName { get; set; }
    public bool? EnableAutoSubDomain { get; set; }
}