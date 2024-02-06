using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Route53.DnsSec;

/// <summary>
/// Assertion for Aws::Route53::DNSSEC.
/// </summary>
public class DnsSecAssertion : ResourceAssertion<DnsSecProperties>
{
    /// <inheritdoc />
    public override string Type => "AWS::Route53::DNSSEC";
}