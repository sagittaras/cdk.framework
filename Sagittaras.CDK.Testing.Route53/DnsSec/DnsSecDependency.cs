using Sagittaras.CDK.Testing.Resources;
using Sagittaras.CDK.Testing.Route53.KSK;

namespace Sagittaras.CDK.Testing.Route53.DnsSec;

/// <summary>
/// Class defining DNS SEC dependency on Key Signing Key.
/// </summary>
public class DnsSecDependency : ResourceDependency
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="kskName">Identification of KSK by its name.</param>
    public DnsSecDependency(string kskName)
    {
        With(new KeySigningKeyAssertion
        {
            Properties = new KeySigningKeyProperties
            {
                Name = kskName
            }
        });
    }
}