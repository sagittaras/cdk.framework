using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Route53;

/// <summary>
/// Properties describing the Hosted Zone in the CloudFormation template.
/// </summary>
public class HostedZoneProperties : ResourceProperties
{
    private string? _name;

    /// <summary>
    /// Name of the hosted zone.
    /// </summary>
    /// <remarks>
    /// Automatically appends the trailing dot that is generated for CloudFormation template.
    /// </remarks>
    public string? Name
    {
        get => _name;
        set => _name = value?.TrimEnd('.') + ".";
    }
}