using Amazon.CDK.AWS.CloudWatch;
using Constructs;
using Sagittaras.CDK.Framework.Extensions;
using Sagittaras.CDK.Framework.Factory;
using Sagittaras.CDK.Framework.Props;

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
        CommonProps = new DeploymentGroupProps
        {
            DeploymentGroupName = groupName
        };
    }

    /// <summary>
    ///     Common props for deployment groups.
    /// </summary>
    private DeploymentGroupProps CommonProps { get; }

    /// <inheritdoc />
    public override TDeploymentGroup Construct()
    {
        PropsMapper.Map(CommonProps, Props);
        return ConstructDeploymentGroup();
    }

    /// <summary>
    ///     Replaces original <see cref="Construct" /> to allow mapping of common properties to the deployment group.
    /// </summary>
    /// <returns></returns>
    protected abstract TDeploymentGroup ConstructDeploymentGroup();

    /// <summary>
    ///     Adds alarms to the deployment group that should watch the deployment.
    /// </summary>
    /// <param name="alarms"></param>
    /// <returns></returns>
    public DeploymentGroupFactory<TDeploymentGroup, TProps> AddAlarms(params IAlarm[] alarms)
    {
        CommonProps.AddAlarms(alarms);

        return this;
    }

    /// <summary>
    ///     Adds alarms to the deployment group that should watch the deployment.
    /// </summary>
    /// <remarks>
    ///     Finds the alarms by their names.
    /// </remarks>
    /// <param name="alarmNames"></param>
    /// <returns></returns>
    public DeploymentGroupFactory<TDeploymentGroup, TProps> AddAlarmsByLookup(params string[] alarmNames)
    {
        IAlarm[] alarms = alarmNames.Select(x => Alarm.FromAlarmName(this, $"alarm-{x.ToResourceId()}", x))
            .ToArray();
        AddAlarms(alarms);

        return this;
    }

    /// <summary>
    ///     Configures whether to ignore alarms during deployment.
    /// </summary>
    /// <param name="ignore"></param>
    /// <returns></returns>
    public DeploymentGroupFactory<TDeploymentGroup, TProps> IgnoreAlarms(bool ignore)
    {
        CommonProps.IgnorePollAlarmsFailure = ignore;

        return this;
    }
}