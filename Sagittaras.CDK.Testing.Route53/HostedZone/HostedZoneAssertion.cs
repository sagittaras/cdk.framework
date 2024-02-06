using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Route53.HostedZone;

/// <summary>
/// Assertion for AWS::Route53::HostedZone.
/// </summary>
public class HostedZoneAssertion : ResourceAssertion<HostedZoneProperties>
{
    /// <inheritdoc />
    public override string Type => "AWS::Route53::HostedZone";

    public HostedZoneAssertion WithName(string name)
    {
        SetProperty(x => x.Name = name);
        return this;
    }
}