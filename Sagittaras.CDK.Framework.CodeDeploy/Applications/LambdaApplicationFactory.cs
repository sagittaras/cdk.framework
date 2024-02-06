using Amazon.CDK.AWS.CodeDeploy;
using Constructs;

namespace Sagittaras.CDK.Framework.CodeDeploy.Applications;

/// <summary>
///     Factory for creating LambdaApplication.
/// </summary>
public class LambdaApplicationFactory : CodeDeployFactory<LambdaApplication, LambdaApplicationProps>
{
    public LambdaApplicationFactory(Construct scope, string applicationName) : base(scope, applicationName)
    {
        Props = new LambdaApplicationProps
        {
            ApplicationName = ApplicationName
        };
    }

    /// <inheritdoc />
    public override LambdaApplicationProps Props { get; }

    /// <inheritdoc />
    public override LambdaApplication Construct()
    {
        return new LambdaApplication(this, "deploy-app", Props);
    }
}