using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Amplify.Domain;

/// <summary>
/// Assertion for AWS::Amplify::Domain.
/// </summary>
public class DomainAssertion : ResourceAssertion<DomainProperties>
{
    /// <inheritdoc />
    public override string Type => "AWS::Amplify::Domain";

    public DomainAssertion WithDomainName(string domainName)
    {
        SetProperty(x => x.DomainName = domainName);
        return this;
    }

    public DomainAssertion WithSubDomain(string prefix)
    {
        SetProperty(x => x.AddSubDomain(new SubDomainProperties
        {
            Prefix = prefix
        }));
        return this;
    }
}