using Amazon.CDK.AWS.IAM;

namespace Sagittaras.CDK.Framework.IAM.Extensions;

/// <summary>
/// Useful extensions for the group construct.
/// </summary>
public static class GroupExtension
{
    /// <summary>
    /// Add enumerable of managed policies to the group.
    /// </summary>
    /// <param name="group"></param>
    /// <param name="policies"></param>
    public static void AddManagedPolicies(this Group group, IEnumerable<IManagedPolicy> policies)
    {
        foreach (IManagedPolicy policy in policies)
        {
            group.AddManagedPolicy(policy);
        }
    }

    /// <summary>
    /// Add enumerable of policy statements to the group.
    /// </summary>
    /// <param name="group"></param>
    /// <param name="statements"></param>
    public static void AddPolicyStatements(this Group group, IEnumerable<PolicyStatement> statements)
    {
        foreach (PolicyStatement statement in statements)
        {
            group.AddToPolicy(statement);
        }
    }
}