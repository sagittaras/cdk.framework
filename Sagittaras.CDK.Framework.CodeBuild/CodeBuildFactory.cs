using Amazon.CDK.AWS.CodeBuild;
using Constructs;
using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;
using Sagittaras.CDK.Framework.Factory;

namespace Sagittaras.CDK.Framework.CodeBuild;

/// <summary>
/// Factory for creating CodeBuild projects.
/// </summary>
public class CodeBuildFactory : ConstructFactory<Project, ProjectProps>
{
    /// <summary>
    /// Definition of the build environment.
    /// </summary>
    private readonly BuildEnvironment _buildEnvironment;

    /// <summary>
    /// Environment variables for the build.
    /// </summary>
    private readonly Dictionary<string, IBuildEnvironmentVariable> _environmentVariables = new();

    /// <summary>
    /// Factory used to generate the buildspec.
    /// </summary>
    private IBuildSpecFactory? _buildSpecFactory;

    public CodeBuildFactory(Construct scope, string projectName) : base(scope, projectName)
    {
        Props = new ProjectProps
        {
            ProjectName = Cloudspace.ResourceName(projectName)
        };

        _buildEnvironment = new BuildEnvironment
        {
            BuildImage = LinuxBuildImage.AMAZON_LINUX_2_5
        };
    }

    /// <summary>
    /// Props describing the project.
    /// </summary>
    public override ProjectProps Props { get; }

    /// <inheritdoc />
    public override Project Construct()
    {
        Props.Environment = _buildEnvironment;
        Props.EnvironmentVariables = _environmentVariables;
        Project project = new(this, "project", Props);
        _buildSpecFactory?.ConfigureProjectPolicies(project);

        return project;
    }

    /// <summary>
    /// Sets description of the codebuild project.
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    public CodeBuildFactory DescribedAs(string description)
    {
        Props.Description = description;
        return this;
    }

    /// <summary>
    /// Define a new GitHub source for the project.
    /// </summary>
    /// <param name="props"></param>
    /// <returns></returns>
    public CodeBuildFactory FromGitHub(GitHubSourceProps props)
    {
        Props.Source = Source.GitHub(props);
        return this;
    }

    /// <summary>
    /// Sets a build image used for the project.
    /// </summary>
    /// <param name="buildImage"></param>
    /// <returns></returns>
    public CodeBuildFactory UseBuildImage(IBuildImage buildImage)
    {
        _buildEnvironment.BuildImage = buildImage;
        return this;
    }

    /// <summary>
    /// Uses privileged mode usable for building docker images.
    /// </summary>
    /// <returns></returns>
    public CodeBuildFactory UsePrivilegedMode()
    {
        _buildEnvironment.Privileged = true;
        return this;
    }

    /// <summary>
    /// Adds a new environment variable to the build or overrides the existing one.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public CodeBuildFactory AddEnvironmentVariable(string name, string value)
    {
        BuildEnvironmentVariable variable = new()
        {
            Type = BuildEnvironmentVariableType.PLAINTEXT,
            Value = value
        };
        _environmentVariables[name] = variable;

        return this;
    }

    /// <summary>
    /// Specifies type of the buildspec factory class from which the buildspec will be generated.
    /// </summary>
    /// <remarks>
    /// This method is automatically creating the instance, so the factory should have a parameterless constructor.
    /// </remarks>
    /// <typeparam name="TBuildSpecFactory"></typeparam>
    /// <returns></returns>
    public CodeBuildFactory UsesBuildSpec<TBuildSpecFactory>(Action<TBuildSpecFactory>? configure = null)
        where TBuildSpecFactory : IBuildSpecFactory, new()
    {
        _buildSpecFactory = new TBuildSpecFactory();
        configure?.Invoke((TBuildSpecFactory)_buildSpecFactory);

        Props.BuildSpec = _buildSpecFactory.ToBuildSpec();
        return this;
    }

    /// <summary>
    /// Passes an existing instance of the buildspec factory class from which the buildspec will be generated.
    /// </summary>
    /// <param name="factory"></param>
    /// <typeparam name="TBuildSpecFactory"></typeparam>
    /// <returns></returns>
    public CodeBuildFactory UsesBuildSpec<TBuildSpecFactory>(TBuildSpecFactory factory)
        where TBuildSpecFactory : IBuildSpecFactory
    {
        _buildSpecFactory = factory;
        Props.BuildSpec = factory.ToBuildSpec();
        return this;
    }

    /// <summary>
    /// Sets the caching options for the project.
    /// </summary>
    /// <param name="cache"></param>
    /// <returns></returns>
    public CodeBuildFactory SetCache(Cache cache)
    {
        Props.Cache = cache;
        return this;
    }
}