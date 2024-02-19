using Amazon.CDK;
using Amazon.CDK.AWS.Amplify.Alpha;
using Constructs;
using Sagittaras.CDK.Framework.Amplify.BuildSpecification.Abstraction;
using Sagittaras.CDK.Framework.Extensions;
using Sagittaras.CDK.Framework.Factory;
using App = Amazon.CDK.AWS.Amplify.Alpha.App;
using AppProps = Amazon.CDK.AWS.Amplify.Alpha.AppProps;

namespace Sagittaras.CDK.Framework.Amplify;

/// <summary>
/// Factory for constructing the Amplify Apps.
/// </summary>
public class AmplifyFactory : ConstructFactory<App, AppProps>
{
    /// <summary>
    /// Dictionary of environment variables for the Amplify App.
    /// </summary>
    private readonly Dictionary<string, string> _envVariables = new();

    /// <summary>
    /// Branches definition for the Amplify App.
    /// </summary>
    private readonly Dictionary<string, BranchOptions> _branches = new();

    /// <summary>
    /// Custom domains assigned to the application.
    /// </summary>
    private readonly List<DomainFactory> _domains = new();

    public AmplifyFactory(Construct scope, string appName) : base(scope, appName)
    {
        Props = new AppProps
        {
            AppName = appName,
            CustomRules = new[]
            {
                new CustomRule(new CustomRuleOptions
                {
                    Source = "/<*>",
                    Target = "/index.html",
                    Status = RedirectStatus.NOT_FOUND_REWRITE
                }),
                new CustomRule(new CustomRuleOptions
                {
                    Source = "</^[^.]+$|\\.(?!(css|gif|ico|jpg|js|png|txt|svg|woff|ttf|map|json)$)([^.]+$)/>",
                    Target = "/index.html",
                    Status = RedirectStatus.REWRITE
                })
            }
        };
    }

    /// <inheritdoc />
    public override AppProps Props { get; }

    /// <inheritdoc />
    public override App Construct()
    {
        Props.EnvironmentVariables = _envVariables;
        App app = new(this, "amplify-app", Props);

        // Assign created branches to dictionary for binding domains to the branches.
        Dictionary<string, Branch> existingBranches = new();
        foreach ((string branch, BranchOptions options) in _branches)
        {
            existingBranches.Add(branch, app.AddBranch(branch, options));
        }

        foreach (DomainFactory factory in _domains)
        {
            app.AddDomain(factory.DomainName.ToResourceId(), factory.Construct(existingBranches));
        }

        return app;
    }

    /// <summary>
    /// Adds a GitHub source code provider to the Amplify App.
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="repository"></param>
    /// <param name="oAuthToken"></param>
    /// <returns></returns>
    public AmplifyFactory FromGitHub(string owner, string repository, SecretValue oAuthToken)
    {
        Props.SourceCodeProvider = new GitHubSourceCodeProvider(new GitHubSourceCodeProviderProps
        {
            Owner = owner,
            Repository = repository,
            OauthToken = oAuthToken
        });
        return this;
    }

    /// <summary>
    /// Enables usage of SSR for the application.
    /// </summary>
    /// <returns></returns>
    public AmplifyFactory UsesSeverSideRendering()
    {
        Props.Platform = Platform.WEB_COMPUTE;
        return this;
    }

    /// <summary>
    /// Assign a new environment variable to the Amplify App.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public AmplifyFactory AddEnvironmentVariable(string name, string value)
    {
        _envVariables.Add(name, value);
        return this;
    }

    /// <summary>
    /// Adds a new branch definition for the application.
    /// </summary>
    /// <param name="branchName"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public AmplifyFactory AddBranch(string branchName, BranchOptions options)
    {
        _branches.Add(branchName, options);
        return this;
    }

    /// <summary>
    /// Adds a new custom domain to the application.
    /// </summary>
    /// <param name="domainName"></param>
    /// <returns></returns>
    public DomainFactory AddDomain(string domainName)
    {
        DomainFactory domain = new(domainName);
        _domains.Add(domain);

        return domain;
    }

    /// <summary>
    /// Assigns a build specification for the Amplify App.
    /// </summary>
    /// <typeparam name="TBuildSpecFactory"></typeparam>
    /// <returns></returns>
    public AmplifyFactory UsesBuildSpec<TBuildSpecFactory>() where TBuildSpecFactory : IAmplifyBuildSpecFactory, new()
    {
        Props.BuildSpec = new TBuildSpecFactory().ToBuildSpecYaml();
        return this;
    }
}