using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Amplify.App;

/// <summary>
/// Assertion for AWS::Amplify::App.
/// </summary>
public class AppAssertion : ResourceAssertion<AppProperties>
{
    /// <inheritdoc />
    public override string Type => "AWS::Amplify::App";

    /// <summary>
    /// Sets the expected name of application.
    /// </summary>
    /// <param name="appName"></param>
    /// <returns></returns>
    public AppAssertion WithAppName(string appName)
    {
        SetProperty(x => x.Name = appName);
        return this;
    }

    /// <summary>
    /// Sets the expected target link to repository.
    /// </summary>
    /// <param name="url">Sets the expected link to the repository.</param>
    /// <returns></returns>
    public AppAssertion FromRepository(string url)
    {
        SetProperty(x => x.Repository = url);
        return this;
    }

    /// <summary>
    /// Sets the expected type of used platform.
    /// </summary>
    /// <param name="platform"></param>
    /// <returns></returns>
    public AppAssertion UsingPlatform(string platform)
    {
        SetProperty(x => x.Platform = platform);
        return this;
    }
}