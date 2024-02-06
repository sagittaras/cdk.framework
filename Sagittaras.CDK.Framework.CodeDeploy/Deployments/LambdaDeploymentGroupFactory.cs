using Amazon.CDK.AWS.CodeDeploy;
using Amazon.CDK.AWS.Lambda;
using Constructs;

namespace Sagittaras.CDK.Framework.CodeDeploy.Deployments;

/// <summary>
///     Factory for creating a deployment group for Lambda function.
/// </summary>
public class LambdaDeploymentGroupFactory : DeploymentGroupFactory<LambdaDeploymentGroup, LambdaDeploymentGroupProps>
{
    public LambdaDeploymentGroupFactory(Construct scope, string groupName, Function function) : base(scope, groupName)
    {
        Props = new LambdaDeploymentGroupProps
        {
            Alias = function.AddAlias("Live", new AliasOptions
            {
                Description = "Alias for the blue/green deployment."
            })
        };
    }

    public LambdaDeploymentGroupFactory(Construct scope, string groupName, Alias alias) : base(scope, groupName)
    {
        Props = new LambdaDeploymentGroupProps
        {
            Alias = alias
        };
    }

    /// <inheritdoc />
    public override LambdaDeploymentGroupProps Props { get; }

    /// <inheritdoc />
    protected override LambdaDeploymentGroup ConstructDeploymentGroup()
    {
        return new LambdaDeploymentGroup(this, "deployment-group", Props);
    }

    /// <summary>
    ///     Makes the deployment group part of the application.
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    public LambdaDeploymentGroupFactory PartOf(ILambdaApplication application)
    {
        Props.Application = application;
        return this;
    }

    /// <summary>
    ///     Makes the deployment group part of the application.
    /// </summary>
    /// <remarks>
    ///     Find the application by its name.
    /// </remarks>
    /// <param name="applicationName"></param>
    /// <returns></returns>
    public LambdaDeploymentGroupFactory PartOf(string applicationName)
    {
        Props.Application = LambdaApplication.FromLambdaApplicationName(this, "application", applicationName);
        return this;
    }

    /// <summary>
    ///     Configures the deployment strategy for the group.
    /// </summary>
    /// <param name="config"></param>
    /// <returns></returns>
    public LambdaDeploymentGroupFactory WithDeploymentConfig(ILambdaDeploymentConfig config)
    {
        Props.DeploymentConfig = config;
        return this;
    }
}