using Amazon.CDK;
using Amazon.CDK.Assertions;
using Sagittaras.CDK.Framework.Route53;
using Sagittaras.CDK.Testing.Extensions;
using Sagittaras.CDK.Testing.KMS;
using Sagittaras.CDK.Testing.KMS.Alias;
using Sagittaras.CDK.Testing.KMS.Key;
using Sagittaras.CDK.Testing.Resources;
using Sagittaras.CDK.Testing.Route53;
using Sagittaras.CDK.Testing.Route53.DnsSec;
using Sagittaras.CDK.Testing.Route53.HostedZone;
using Sagittaras.CDK.Testing.Route53.KSK;
using Sagittaras.CDK.Testing.Route53.RecordSet;
using Xunit;

namespace Sagittaras.CDK.Tests.Route53;

/// <summary>
/// Test creation of Public Hosted zone.
/// </summary>
public class PublicHostedZoneTest : ConstructTest
{
    private const string Domain = "example.com";
    private const string Comment = "";

    /// <summary>
    /// Tests basic usage of the factory for Hosted Zone.
    /// </summary>
    [Fact]
    public void Test_BaseFactoryUsage()
    {
        new PublicHostedZoneFactory(Stack, Domain, Comment)
            .Construct();

        Template template = StackTemplate;
        template.Assert(new HostedZoneAssertion
        {
            Properties = new HostedZoneProperties
            {
                Name = Domain
            }
        });
        template.AssertCount<HostedZoneAssertion>(1);
        template.AssertCount<RecordSetAssertion>(0);
    }

    /// <summary>
    /// Tests the creation of Hosted Zone with DNSSEC enabled.
    /// </summary>
    [Fact]
    public void Test_DNSSEC()
    {
        new PublicHostedZoneFactory(Stack, Domain, Comment)
            .WithDnsSec()
            .Construct();

        Template template = StackTemplate;
        template.AssertCount<KeyAssertion>(1);
        template.Assert(new AliasAssertion
        {
            Properties = new AliasProperties
            {
                AliasName = "alias/examplecom-key"
            }
        });
        template.Assert(new KeySigningKeyAssertion
        {
            Properties = new KeySigningKeyProperties
            {
                Status = "ACTIVE"
            }
        });
        template.Assert(new DnsSecAssertion
        {
            DependsOn = new DnsSecDependency("examplecom")
        });
    }
}