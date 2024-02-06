using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;
using Sagittaras.CDK.Framework.CodeDeploy.Deployments;

namespace Sagittaras.CDK.Framework.Lambda.Extensions;

/// <summary>
///     Extension method for function alias.
/// </summary>
public static class AliasExtension
{
    /// <summary>
    ///     Adds a deployment group to the alias.
    /// </summary>
    /// <remarks>
    ///     Method itself constructs the deployment group. It is not necessary to call <see cref="LambdaDeploymentGroupFactory.Construct" /> method.
    /// </remarks>
    /// <param name="alias"></param>
    /// <param name="deploymentGroup"></param>
    /// <param name="configure">Callback to configure the deployment group.</param>
    /// <returns></returns>
    public static Alias HasDeployment(this Alias alias, string deploymentGroup, Action<LambdaDeploymentGroupFactory> configure)
    {
        LambdaDeploymentGroupFactory factory = new(alias, deploymentGroup, alias);
        configure.Invoke(factory);
        factory.Construct();

        return alias;
    }

    /// <summary>
    ///     Creates an API Gateway REST API and uses the alias as a handler.
    /// </summary>
    /// <param name="alias"></param>
    /// <param name="apiName"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public static Alias AsRestApiHandler(this Alias alias, string apiName, string? description = null)
    {
        _ = new LambdaRestApi(alias, "gateway-proxy", new LambdaRestApiProps
        {
            RestApiName = Cloudspace.ResourceName(apiName),
            Description = description,
            Handler = alias
        });

        return alias;
    }
}