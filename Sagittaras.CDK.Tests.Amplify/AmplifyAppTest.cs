using Amazon.CDK;
using Amazon.CDK.Assertions;
using Amazon.CDK.AWS.Amplify.Alpha;
using Sagittaras.CDK.Framework.Amplify;
using Sagittaras.CDK.Testing.Amplify.App;
using Sagittaras.CDK.Testing.Amplify.Branch;
using Sagittaras.CDK.Testing.Amplify.Domain;
using Xunit;

namespace Sagittaras.CDK.Tests.Amplify;

/// <summary>
/// Tests creation of the Amplify Apps through framework.
/// </summary>
public class AmplifyAppTest : ConstructTest
{
    /// <summary>
    /// Name of the application.
    /// </summary>
    private const string AppName = "Testing App";

    /// <summary>
    /// Tests the most basic definition of amplify app.
    /// </summary>
    [Fact]
    public void Test_BaseConstruct()
    {
        new AmplifyFactory(Stack, AppName)
            .FromGitHub("sagittaras", "website", SecretValue.SecretsManager("arn:aws:secretsmanager:eu-central-1"))
            .AddBranch("main", new BranchOptions
            {
                BranchName = "main"
            })
            .Construct();

        Template template = StackTemplate;
        new AppAssertion()
            .AssertCount(template, 1);
        
        new BranchAssertion()
            .AssertCount(template, 1);

        new AppAssertion()
            .WithAppName(AppName)
            .FromRepository("https://github.com/sagittaras/website")
            .UsingPlatform("WEB")
            .Assert(template);

        new BranchAssertion()
            .WithBranchName("main")
            .HasAutoBuildEnabled(true)
            .Assert(template);
    }

    /// <summary>
    /// Tests the assignment of the domain to the Amplify App.
    /// </summary>
    [Fact]
    public void Test_DomainAssignment()
    {
        AmplifyFactory factory = new AmplifyFactory(Stack, AppName)
            .FromGitHub("sagittaras", "website", SecretValue.SecretsManager("arn:aws:secretsmanager:eu-central-1"))
            .AddBranch("main", new BranchOptions
            {
                BranchName = "main"
            });

        factory.AddDomain("example.com")
            .AddSubDomain(string.Empty, "main")
            .AddSubDomain("www", "main")
            ;

        factory.Construct();

        Template template = StackTemplate;
        
        new AppAssertion()
            .AssertCount(template, 1);
        
        new DomainAssertion()
            .AssertCount(template, 1);

        new DomainAssertion()
            .WithDomainName("example.com")
            .WithSubDomain(string.Empty)
            .WithSubDomain("www")
            .Assert(template);
    }
}