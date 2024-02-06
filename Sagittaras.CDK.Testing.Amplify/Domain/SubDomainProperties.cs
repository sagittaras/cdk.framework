using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Amplify.Domain;

/// <summary>
/// Sub-properties of AWS::Amplify::Domain, specifying the subdomain.
/// </summary>
public class SubDomainProperties : ResourceProperties
{
    public object? BranchName { get; set; }
    public string? Prefix { get; set; }
}