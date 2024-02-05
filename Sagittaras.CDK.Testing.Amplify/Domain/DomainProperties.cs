using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Amplify.Domain;

/// <summary>
/// Properties for AWS::Amplify::Domain.
/// </summary>
public class DomainProperties : ResourceProperties
{
    private List<SubDomainProperties> _subDomainSettings = new();

    public string? DomainName { get; set; }
    public bool? EnableAutoSubDomain { get; set; }
    public SubDomainProperties[] SubDomainSettings
    {
        get => _subDomainSettings.ToArray();
        set => _subDomainSettings = new List<SubDomainProperties>(value);
    }

    public void AddSubDomain(SubDomainProperties subDomain)
    {
        _subDomainSettings.Add(subDomain);
    }
}