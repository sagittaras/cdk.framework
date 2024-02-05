using Amazon.CDK;
using Amazon.CDK.Assertions;
using Sagittaras.CDK.Framework.Route53;
using Sagittaras.CDK.Testing.Extensions;
using Sagittaras.CDK.Testing.Route53;
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
        template.ResourceCountIs("AWS::Route53::HostedZone", 1);
        template.ResourceCountIs("AWS::Route53::RecordSet", 0);
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
        template.ResourceCountIs("AWS::KMS::Key", 1);
        template.HasResourceProperties("AWS::KMS::Alias", new Dictionary<string, object>
        {
            { "AliasName", "alias/examplecom-key" }
        });

        template.HasResourceProperties("AWS::Route53::KeySigningKey", new Dictionary<string, object>
        {
            { "Status", "ACTIVE" }
        });

        template.HasResource("AWS::Route53::DNSSEC", new Dictionary<string, object>
        {
            { "DependsOn", new[] { template.FindResources("AWS::Route53::KeySigningKey").First().Key } }
        });
    }
}