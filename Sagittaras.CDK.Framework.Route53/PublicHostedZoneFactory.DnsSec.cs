using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.KMS;
using Amazon.CDK.AWS.Route53;
using Sagittaras.CDK.Framework.Extensions;

namespace Sagittaras.CDK.Framework.Route53;

/// <summary>
/// Partial class definition adding ability to enable DNS security for the hosted zone.
/// </summary>
public partial class PublicHostedZoneFactory
{
    /// <summary>
    /// Defines if the hosted zone has DNS security enabled.
    /// </summary>
    private bool _hasDnsSec;

    /// <summary>
    /// Enables DNS Sec for the hosted zone.
    /// </summary>
    /// <remarks>
    /// - DNS SEC for root zones can be enabled once the NS records are bound to the parent zone (sagittaras.com -> com zone).
    /// - DS Record must be added to the parent zone once the DNS SEC is created for the domain.
    /// </remarks>
    /// <returns></returns>
    public PublicHostedZoneFactory WithDnsSec()
    {
        _hasDnsSec = true;
        return this;
    }

    /// <summary>
    /// Enables DNS security for the hosted zone.
    /// </summary>
    private void EnableDnsSec(IHostedZone zone)
    {
        if (!_hasDnsSec)
        {
            return;
        }

        Key domainKey = ConstructDomainKey(zone);
        CfnKeySigningKey ksk = ConstructSigningKey(zone, domainKey);
        CfnDNSSEC dnssec = new(this, "dnssec", new CfnDNSSECProps
        {
            HostedZoneId = zone.HostedZoneId
        });
        dnssec.AddDependency(ksk);
    }

    /// <summary>
    /// Construct the KMS key for the hosted zone to enable DNS SEC.
    /// </summary>
    /// <param name="zone"></param>
    /// <returns></returns>
    private Key ConstructDomainKey(IHostedZone zone)
    {
        Key domainKey = new(this, "kms-key", new KeyProps
        {
            Description = $"Key of {zone.ZoneName} hosted zone to enable DNS SEC.",
            KeySpec = KeySpec.ECC_NIST_P256,
            KeyUsage = KeyUsage.SIGN_VERIFY,
            EnableKeyRotation = false,
            Alias = $"{zone.ZoneName.ToResourceId()}-key"
        });

        domainKey.AddToResourcePolicy(new PolicyStatement(new PolicyStatementProps
        {
            Sid = "Allow Route 53 DNSSEC to use the key",
            Effect = Effect.ALLOW,
            Principals = new IPrincipal[]
            {
                new ServicePrincipal("dnssec-route53.amazonaws.com")
            },
            Actions = new[]
            {
                "kms:DescribeKey",
                "kms:GetPublicKey",
                "kms:Sign"
            },
            Resources = new[]
            {
                "*"
            },
            Conditions = new Dictionary<string, object>
            {
                {
                    "StringEquals",
                    new Dictionary<string, object>
                    {
                        {
                            "aws:SourceAccount",
                            Cloudspace.AccountId
                        }
                    }
                }
            }
        }));

        domainKey.AddToResourcePolicy(new PolicyStatement(new PolicyStatementProps
        {
            Sid = "Allow Route 53 DNSSEC to CreateGrant",
            Effect = Effect.ALLOW,
            Principals = new IPrincipal[]
            {
                new ServicePrincipal("dnssec-route53.amazonaws.com")
            },
            Actions = new[]
            {
                "kms:CreateGrant"
            },
            Resources = new[]
            {
                "*"
            },
            Conditions = new Dictionary<string, object>
            {
                {
                    "Bool",
                    new Dictionary<string, object>
                    {
                        {
                            "kms:GrantIsForAWSResource",
                            "true"
                        }
                    }
                }
            }
        }));

        return domainKey;
    }

    /// <summary>
    /// Construct the domain's signing key from KMS key.
    /// </summary>
    /// <param name="zone"></param>
    /// <param name="domainKey"></param>
    private CfnKeySigningKey ConstructSigningKey(IHostedZone zone, IKey domainKey)
    {
        return new CfnKeySigningKey(this, "signing-key", new CfnKeySigningKeyProps
        {
            HostedZoneId = zone.HostedZoneId,
            KeyManagementServiceArn = domainKey.KeyArn,
            Name = zone.ZoneName.ToResourceId(),
            Status = "ACTIVE"
        });
    }
}