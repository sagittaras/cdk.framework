using Amazon.CDK.Assertions;
using Amazon.CDK.AWS.CodeDeploy;
using Amazon.CDK.AWS.Lambda;
using Sagittaras.CDK.Framework.CodeDeploy.Deployments;
using Sagittaras.CDK.Testing.CodeDeploy.DeploymentGroup;
using Xunit;

namespace Sagittaras.CDK.Tests.CodeDeploy;

public class LambdaDeploymentGroupTest : ConstructTest
{
    private const string GroupName = "Staging";

    /// <summary>
    ///     Some basic instance of a Lambda function.
    /// </summary>
    private Function LambdaFunction => new(Stack, "lambda", new FunctionProps
    {
        Runtime = Runtime.NODEJS_20_X,
        Handler = "index.handler",
        Code = Code.FromInline("exports.handler = async function(event, context) { return 'Hello, CDK!'; };")
    });

    /// <summary>
    ///     Tests creation of the most basic deployment group construct.
    /// </summary>
    [Fact]
    public void Test_BaseConstruct()
    {
        new LambdaDeploymentGroupFactory(Stack, GroupName, LambdaFunction)
            .Construct();

        Template template = StackTemplate;

        new DeploymentGroupAssertion()
            .WithGroupName(GroupName)
            .Assert(template);
    }

    /// <summary>
    ///     Tests deployment group with assigned alarms & deployment config.
    /// </summary>
    [Fact]
    public void Test_BetterDeployment()
    {
        new LambdaDeploymentGroupFactory(Stack, GroupName, LambdaFunction)
            .WithDeploymentConfig(LambdaDeploymentConfig.CANARY_10PERCENT_30MINUTES)
            .AddAlarmsByLookup("Alarm1", "Alarm2", "Alarm3")
            .Construct();

        Template template = StackTemplate;

        new DeploymentGroupAssertion()
            .WithGroupName(GroupName)
            .WithLambdaDeploymentConfig(LambdaDeploymentConfig.CANARY_10PERCENT_30MINUTES)
            .HasAlarmsEnabled(true)
            .Assert(template);
    }
}