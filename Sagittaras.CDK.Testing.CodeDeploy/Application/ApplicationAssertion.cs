using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.CodeDeploy.Application;

/// <summary>
///     Assertions for AWS::CodeDeploy::Application.
/// </summary>
public class ApplicationAssertion : ResourceAssertion<ApplicationProperties>
{
    /// <inheritdoc />
    public override string Type => "AWS::CodeDeploy::Application";

    public ApplicationAssertion WithApplicationName(string applicationName)
    {
        SetProperty(x => x.ApplicationName = applicationName);
        return this;
    }

    public ApplicationAssertion UsesPlatform(ComputePlatform platform)
    {
        SetProperty(x => x.ComputePlatform = platform);
        return this;
    }
}