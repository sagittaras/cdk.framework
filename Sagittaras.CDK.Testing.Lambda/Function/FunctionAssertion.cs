using Amazon.CDK;
using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Lambda.Function;

/// <summary>
///     Assertion for AWS::Lambda::Function.
/// </summary>
public class FunctionAssertion : ResourceAssertion<FunctionProperties>
{
    /// <inheritdoc />
    public override string Type => "AWS::Lambda::Function";

    /// <summary>
    ///     Sets the expected name of the lambda function.
    /// </summary>
    /// <param name="functionName"></param>
    /// <returns></returns>
    public FunctionAssertion WithFunctionName(string functionName)
    {
        SetProperty(x => x.FunctionName = functionName);
        return this;
    }

    /// <summary>
    ///     Sets expected memory size configuration.
    /// </summary>
    /// <param name="memorySize"></param>
    /// <returns></returns>
    public FunctionAssertion WithMemorySize(int memorySize)
    {
        SetProperty(x => x.MemorySize = memorySize);
        return this;
    }

    /// <summary>
    ///     Sets the expected timeout limit.
    /// </summary>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public FunctionAssertion WithTimeout(Duration timeout)
    {
        SetProperty(x => x.Timeout = (int)timeout.ToSeconds());
        return this;
    }

    /// <summary>
    ///     Sets the tracing configuration to expect enabled X-Ray tracing.
    /// </summary>
    /// <returns></returns>
    public FunctionAssertion WithXRayTracing()
    {
        SetProperty(x => x.TracingConfig = new TracingConfigProperties
        {
            Mode = TracingConfigMode.Active
        });
        return this;
    }

    /// <summary>
    ///     Expects the function to be a Docker image.
    /// </summary>
    /// <returns></returns>
    public FunctionAssertion IsDockerImage()
    {
        SetProperty(x => x.PackageType = PackageType.Image);
        return this;
    }

    /// <summary>
    ///     Sets expected environment variable.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public FunctionAssertion HasEnvironmentVariable(string key, string value)
    {
        SetProperty(x =>
        {
            x.Environment ??= new EnvironmentProperties
            {
                Variables = new Dictionary<string, string>()
            };

            x.Environment.Variables!.Add(key, value);
        });
        return this;
    }
}