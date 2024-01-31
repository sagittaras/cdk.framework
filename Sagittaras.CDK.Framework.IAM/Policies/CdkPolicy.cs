using Amazon.CDK.AWS.IAM;

namespace Sagittaras.CDK.Framework.IAM.Policies;

/// <summary>
/// Static class containing references of commonly used policy statements.
/// </summary>
public static class CdkPolicy
{
    /// <summary>
    /// Policy statement that allows to describe availability zones.
    /// </summary>
    public static PolicyStatement DescribeAvailabilityZones => new(new PolicyStatementProps
    {
        Sid = "DescribeAvailabilityZones",
        Effect = Effect.ALLOW,
        Actions = new[] { "ec2:DescribeAvailabilityZones" },
        Resources = new[] { "*" }
    });

    /// <summary>
    /// Policy statement that allows to assume CDK roles.
    /// </summary>
    public static PolicyStatement AssumeRole => new(new PolicyStatementProps
    {
        Sid = "AssumeCdkRoles",
        Effect = Effect.ALLOW,
        Actions = new[] { "sts:AssumeRole" },
        Resources = new[] { $"arn:aws:iam::{Cloudspace.AccountId}:role/cdk-*" }
    });
}