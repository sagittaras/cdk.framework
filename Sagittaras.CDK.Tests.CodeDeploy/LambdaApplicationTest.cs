using Amazon.CDK.Assertions;
using Sagittaras.CDK.Framework;
using Sagittaras.CDK.Framework.CodeDeploy.Applications;
using Sagittaras.CDK.Testing.CodeDeploy.Application;
using Xunit;

namespace Sagittaras.CDK.Tests.CodeDeploy;

public class LambdaApplicationTest : ConstructTest
{
    private const string ApplicationName = "LambdaTest";

    /// <summary>
    ///     Tests most basic creation of lambda application.
    /// </summary>
    [Fact]
    public void Test_BaseConstruct()
    {
        new LambdaApplicationFactory(Stack, ApplicationName)
            .Construct();

        Template template = StackTemplate;

        new ApplicationAssertion()
            .WithApplicationName(Cloudspace.ResourceName(ApplicationName))
            .UsesPlatform(ComputePlatform.Lambda)
            .Assert(template);
    }
}