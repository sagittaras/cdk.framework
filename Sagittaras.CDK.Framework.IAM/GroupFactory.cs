using Amazon.CDK.AWS.IAM;
using Constructs;
using Sagittaras.CDK.Framework.Factory;
using Sagittaras.CDK.Framework.IAM.Extensions;

namespace Sagittaras.CDK.Framework.IAM;

/// <summary>
/// Construct factory for creating an IAM group.
/// </summary>
public class GroupFactory : ConstructFactory<Group, GroupProps>
{
    /// <summary>
    /// List of manages policies that will be assigned to the group.
    /// </summary>
    private readonly List<IManagedPolicy> _managedPolicies = new();

    /// <summary>
    /// List of policy statements that will be assigned to the group.
    /// </summary>
    private readonly List<PolicyStatement> _policyStatements = new();

    public GroupFactory(Construct scope, string groupName) : base(scope, groupName)
    {
        Props = new GroupProps
        {
            GroupName = groupName
        };
    }

    /// <summary>
    /// Configured basic props, from which the group will be created.
    /// </summary>
    public override GroupProps Props { get; }

    /// <summary>
    /// Adds a managed policy for the group from the given ARN.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="arn"></param>
    /// <returns></returns>
    public GroupFactory WithManagedPolicyFromArn(string id, string arn)
    {
        _managedPolicies.Add(ManagedPolicy.FromManagedPolicyArn(this, id, arn));
        return this;
    }

    /// <summary>
    /// Adds a policy statement for the group from given props.
    /// </summary>
    /// <param name="props"></param>
    /// <returns></returns>
    public GroupFactory WithPolicyStatement(PolicyStatementProps props)
    {
        _policyStatements.Add(new PolicyStatement(props));
        return this;
    }

    /// <summary>
    /// Construct the group with ID defined from its name.
    /// </summary>
    /// <returns></returns>
    public override Group Construct()
    {
        Group group = new(this, "group", Props);
        group.AddManagedPolicies(_managedPolicies);
        group.AddPolicyStatements(_policyStatements);

        return group;
    }
}