using System.Reflection;
using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;
using Constructs;
using Sagittaras.CDK.Framework.Factory;

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

        Type optionsType = Options.GetType();
        Type propsType = props.GetType();

        Dictionary<string, object> definedOptions = optionsType.GetProperties()
            .Select(x => new KeyValuePair<string, object?>(x.Name, x.GetValue(Options)))
            .Where(x => x.Value is not null)
            .ToDictionary(x => x.Key, x => x.Value!);

        Dictionary<string, PropertyInfo> targetProps = propsType.GetProperties()
            .Where(x => definedOptions.ContainsKey(x.Name))
            .ToDictionary(x => x.Name, x => x);

        foreach ((string name, object value) in definedOptions)
        {
            targetProps[name].SetValue(props, value);
        }
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
}