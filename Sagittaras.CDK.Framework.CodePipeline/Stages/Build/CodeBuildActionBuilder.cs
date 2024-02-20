using Amazon.CDK.AWS.CodeBuild;
using Amazon.CDK.AWS.CodePipeline.Actions;

namespace Sagittaras.CDK.Framework.CodePipeline.Stages.Build;

/// <summary>
/// Factory building the code build action for pipeline stage.
/// </summary>
public class CodeBuildActionBuilder : ActionBuilder<CodeBuildAction>
{
    /// <summary>
    /// Stage builder in which context the action is created.
    /// </summary>
    private readonly PipelineStageBuilder _builder;

    /// <summary>
    /// Props to create the action.
    /// </summary>
    private readonly CodeBuildActionProps _props;

    /// <summary>
    /// List of output artifact names.
    /// </summary>
    private readonly List<string> _outputs = new();

    /// <summary>
    /// CodeBuild environment variables specified for the action.
    /// </summary>
    private readonly Dictionary<string, IBuildEnvironmentVariable> _buildVariables = new();

    public CodeBuildActionBuilder(PipelineStageBuilder builder, string name) : base(builder, name)
    {
        _builder = builder;
        _props = new CodeBuildActionProps
        {
            ActionName = name
        };
    }

    /// <inheritdoc />
    public override CodeBuildAction Construct()
    {
        _props.Outputs = _outputs
            .Select(_builder.UseArtifact)
            .ToArray();

        if (_buildVariables.Any())
        {
            _props.EnvironmentVariables = _buildVariables;
        }

        return new CodeBuildAction(_props);
    }

    /// <inheritdoc />
    public override IActionBuilder RunOrder(int order)
    {
        _props.RunOrder = order;
        return this;
    }

    /// <summary>
    /// Sets the CodeBuild project used for the action.
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    public CodeBuildActionBuilder UsesProject(IProject project)
    {
        _props.Project = project;

        return this;
    }

    /// <summary>
    /// Configures the used input artifact.
    /// </summary>
    /// <param name="artifactName"></param>
    /// <returns></returns>
    public CodeBuildActionBuilder UsesInputArtifact(string artifactName)
    {
        _props.Input = UseArtifact(artifactName);

        return this;
    }

    /// <summary>
    /// Adds an output artifact to the action.
    /// </summary>
    /// <param name="artifactName"></param>
    /// <returns></returns>
    public CodeBuildActionBuilder HasOutputArtifact(string artifactName)
    {
        _outputs.Add(artifactName);

        return this;
    }

    /// <summary>
    /// Adds multiple output artifacts to the action.
    /// </summary>
    /// <param name="artifactNames"></param>
    /// <returns></returns>
    public CodeBuildActionBuilder HasOutputArtifacts(params string[] artifactNames)
    {
        _outputs.AddRange(artifactNames);

        return this;
    }

    /// <summary>
    /// Adds environment variable specified for the action.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public CodeBuildActionBuilder AddEnvironmentVariable(string name, string value)
    {
        _buildVariables[name] = new BuildEnvironmentVariable
        {
            Type = BuildEnvironmentVariableType.PLAINTEXT,
            Value = value
        };
        return this;
    }

    /// <summary>
    /// Adds a dictionary set of environment variables specified for the action.
    /// </summary>
    /// <param name="variables"></param>
    /// <returns></returns>
    public CodeBuildActionBuilder AddEnvironmentVariables(IDictionary<string, string> variables)
    {
        foreach ((string name, string value) in variables)
        {
            AddEnvironmentVariable(name, value);
        }

        return this;
    }
}