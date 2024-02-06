using Amazon.CDK.AWS.CloudWatch;
using Amazon.CDK.AWS.CodeDeploy;

namespace Sagittaras.CDK.Framework.CodeDeploy.Deployments;

/// <summary>
///     Custom class to group common properties of the deployment groups of different types,
///     to overcome missing base class for deployment groups.
/// </summary>
public class DeploymentGroupProps
{
    private readonly List<IAlarm> _alarms = new();

    public IAlarm[]? Alarms
    {
        get => _alarms.Any() ? _alarms.ToArray() : null;
        set
        {
            if (value is null) return;

            _alarms.AddRange(value);
        }
    }

    public AutoRollbackConfig AutoRollback { get; set; } = new()
    {
        FailedDeployment = true,
        StoppedDeployment = true
    };

    public string? DeploymentGroupName { get; set; }
    public bool? IgnorePollAlarmsFailure { get; set; }

    public void AddAlarms(params IAlarm[] alarms)
    {
        _alarms.AddRange(alarms);
    }
}