using Amazon.CDK.AWS.Amplify.Alpha;
using Sagittaras.CDK.Framework.Amplify.BuildSpecification.Abstraction;
using Sagittaras.CDK.Framework.Factory;

namespace Sagittaras.CDK.Framework.Amplify;

/// <summary>
/// Factory for constructing the Options that describes the branch of the Amplify App.
/// </summary>
public class BranchFactory : ICdkFactory<BranchOptions>
{
    /// <summary>
    /// Instance of modified branch options.
    /// </summary>
    private readonly BranchOptions _options;

    /// <summary>
    /// Dictionary of environment variable overrides for the branch.
    /// </summary>
    private readonly Dictionary<string, string> _envVariables = new();

    public BranchFactory(string branchName)
    {
        _options = new BranchOptions
        {
            BranchName = branchName
        };
    }

    /// <inheritdoc />
    public BranchOptions Construct()
    {
        if (_envVariables.Count > 0)
        {
            _options.EnvironmentVariables = _envVariables;
        }

        return _options;
    }

    /// <summary>
    /// Sets description of the branch.
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    public BranchFactory DescribedAs(string description)
    {
        _options.Description = description;
        return this;
    }

    /// <summary>
    /// Adds an environment variable override for the branch.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public BranchFactory AddEnvironmentVariable(string name, string value)
    {
        _envVariables[name] = value;
        return this;
    }

    /// <summary>
    /// Adds an override for the used build specification for the branch.
    /// </summary>
    /// <typeparam name="TBuildSpecFactory"></typeparam>
    /// <returns></returns>
    public BranchFactory UsesBuildSpec<TBuildSpecFactory>() where TBuildSpecFactory : IAmplifyBuildSpecFactory, new()
    {
        _options.BuildSpec = new TBuildSpecFactory().ToBuildSpecYaml();
        return this;
    }

    /// <summary>
    /// Disable the auto build feature for the branch.
    /// </summary>
    /// <returns></returns>
    public BranchFactory NoAutoBuild()
    {
        _options.AutoBuild = false;
        return this;
    }
}