using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.Lambda;
using Constructs;
using Sagittaras.CDK.Framework.Factory;
using Sagittaras.CDK.Framework.Props;

namespace Sagittaras.CDK.Framework.Lambda;

/// <summary>
/// Base Lambda factory containing common property definitions.
/// </summary>
/// <typeparam name="TFunction">Type of the target lambda function.</typeparam>
/// <typeparam name="TProps">Type of corresponding properties to target type.</typeparam>
public abstract class LambdaFactory<TFunction, TProps> : ConstructFactory<TFunction, TProps>
    where TFunction : IFunction
    where TProps : class, IFunctionOptions
{
    protected LambdaFactory(Construct scope, string functionName) : base(scope, functionName)
    {
        Options = new FunctionOptions
        {
            FunctionName = Cloudspace.ResourceName(functionName)
        };
    }

    /// <summary>
    /// Definition of common properties for the function.
    /// </summary>
    private FunctionOptions Options { get; }

    /// <summary>
    /// List of security groups to which the lambda should be assigned.
    /// </summary>
    private List<ISecurityGroup> SecurityGroups { get; } = new();

    /// <summary>
    /// Environment variables to be set for the function.
    /// </summary>
    private Dictionary<string, string> EnvironmentVariables { get; } = new();

    /// <summary>
    /// Maps the <see cref="Options"/> used as storage of common attributes to the props.
    /// </summary>
    /// <param name="props"></param>
    protected void MapCommonProperties(TProps props)
    {
        if (EnvironmentVariables.Any())
        {
            Options.Environment = EnvironmentVariables;
        }

        if (SecurityGroups.Any())
        {
            Options.SecurityGroups = SecurityGroups.ToArray();
        }

        PropsMapper.Map(Options, props);
    }

    /// <summary>
    /// Sets the description of Lambda function.
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    public LambdaFactory<TFunction, TProps> DescribedAs(string description)
    {
        Options.Description = description;
        return this;
    }

    /// <summary>
    /// Configures the timeout for the function.
    /// </summary>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public LambdaFactory<TFunction, TProps> WithTimeout(Duration timeout)
    {
        Options.Timeout = timeout;
        return this;
    }

    /// <summary>
    /// Sets the memory size for the function.
    /// </summary>
    /// <param name="memorySize"></param>
    /// <returns></returns>
    public LambdaFactory<TFunction, TProps> WithMemorySize(double memorySize)
    {
        Options.MemorySize = memorySize;
        return this;
    }

    /// <summary>
    /// Adds new environment variable to the function.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public LambdaFactory<TFunction, TProps> AddEnvironmentVariable(string key, string value)
    {
        EnvironmentVariables[key] = value;
        return this;
    }

    /// <summary>
    /// Enables X-Ray tracing for the function.
    /// </summary>
    /// <returns></returns>
    public LambdaFactory<TFunction, TProps> WithXRay()
    {
        Options.Tracing = Tracing.ACTIVE;
        return this;
    }

    /// <summary>
    ///     Configures the Lambda function to use the default VPC.
    /// </summary>
    /// <param name="subnetIds">IDs of subnets to which the Lambda function should be connected.</param>
    /// <returns></returns>
    public LambdaFactory<TFunction, TProps> ConnectToDefaultVpc(params string[] subnetIds)
    {
        Options.Vpc = Vpc.FromLookup(this, "default-vpc", new VpcLookupOptions
        {
            IsDefault = true
        });

        if (!subnetIds.Any()) return this;

        Options.VpcSubnets = new SubnetSelection
        {
            Subnets = subnetIds.Select(subnetId => Subnet.FromSubnetAttributes(this, subnetId, new SubnetAttributes
            {
                SubnetId = subnetId
            })).ToArray()
        };

        return this;
    }

    /// <summary>
    ///     Allows the function to be connected to the public subnet.
    /// </summary>
    /// <remarks>
    ///     See: https://stackoverflow.com/questions/52992085/why-cant-an-aws-lambda-function-inside-a-public-subnet-in-a-vpc-connect-to-the/52994841#52994841
    /// </remarks>
    /// <returns></returns>
    public LambdaFactory<TFunction, TProps> AllowPublicSubnet()
    {
        Options.AllowPublicSubnet = true;
        return this;
    }

    /// <summary>
    /// Adds a security group for the lambda function.
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    public LambdaFactory<TFunction, TProps> AddSecurityGroup(ISecurityGroup group)
    {
        SecurityGroups.Add(group);
        return this;
    }

    /// <summary>
    /// Adds a security group created by lookup from its ID.
    /// </summary>
    /// <param name="groupId"></param>
    /// <returns></returns>
    public LambdaFactory<TFunction, TProps> AddSecurityGroup(string groupId)
    {
        SecurityGroups.Add(SecurityGroup.FromSecurityGroupId(this, groupId, groupId));
        return this;
    }
}