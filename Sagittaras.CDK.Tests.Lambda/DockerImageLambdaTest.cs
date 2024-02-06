using Amazon.CDK;
using Amazon.CDK.Assertions;
using Sagittaras.CDK.Framework;
using Sagittaras.CDK.Framework.Lambda;
using Sagittaras.CDK.Testing.Lambda.Function;
using Xunit;

namespace Sagittaras.CDK.Tests.Lambda;

public class DockerImageLambdaTest : ConstructTest
{
    private const string FunctionName = "Test";

    /// <summary>
    ///     Tests basic definition of lambda function with usage of Docker image.
    /// </summary>
    [Fact]
    public void Test_BaseConstruct()
    {
        new DockerImageLambdaFactory(Stack, FunctionName)
            .FromEcr("ubuntu", "latest")
            .Construct();

        Template template = StackTemplate;

        new FunctionAssertion()
            .WithFunctionName(Cloudspace.ResourceName(FunctionName))
            .IsDockerImage()
            .Assert(template);
    }

    /// <summary>
    ///     Tests creation of lambda function with defined custom properties for environment.
    /// </summary>
    [Fact]
    public void Test_CustomEnvironment()
    {
        const int memorySize = 1024;
        Duration timeout = Duration.Minutes(1);

        new DockerImageLambdaFactory(Stack, FunctionName)
            .FromEcr("ubuntu", "latest")
            .WithTimeout(timeout)
            .WithMemorySize(memorySize)
            .WithXRay()
            .AddEnvironmentVariable("Foo", "Bar")
            .Construct();

        Template template = StackTemplate;

        new FunctionAssertion()
            .WithMemorySize(memorySize)
            .WithTimeout(timeout)
            .WithXRayTracing()
            .HasEnvironmentVariable("Foo", "Bar")
            .Assert(template);
    }
}