using Constructs;
using Sagittaras.CDK.Framework.Factory;

namespace Sagittaras.CDK.Framework.CodeDeploy.Deployments;

/// <summary>
///     Base factory for creating a deployment group.
/// </summary>
/// <typeparam name="TDeploymentGroup"></typeparam>
/// <typeparam name="TProps"></typeparam>
public abstract class DeploymentGroupFactory<TDeploymentGroup, TProps> : ConstructFactory<TDeploymentGroup, TProps>
    where TDeploymentGroup : IConstruct
    where TProps : class
{
    protected DeploymentGroupFactory(Construct scope, string groupName) : base(scope, groupName)
    {
    }
}