using Amazon.CDK.AWS.CodePipeline.Actions;
using Sagittaras.CDK.Framework.Factory;

namespace Sagittaras.CDK.Framework.CodePipeline.Stages.Source;

/// <summary>
/// Builder factory used to define a source connection with code star.
/// </summary>
public class CodeStarFactory : CdkFactory<CodeStarConnectionsSourceAction>
{
    /// <summary>
    /// Builder in which scope the source is being defined.
    /// </summary>
    private readonly PipelineStageBuilder _builder;

    /// <summary>
    /// Props used to create the source.
    /// </summary>
    private readonly CodeStarConnectionsSourceActionProps _props;

    public CodeStarFactory(PipelineStageBuilder builder, string name) : base(builder, name)
    {
        _builder = builder;
        _props = new CodeStarConnectionsSourceActionProps
        {
            ActionName = name,
            CodeBuildCloneOutput = true
        };
    }

    /// <inheritdoc />
    public override CodeStarConnectionsSourceAction Construct()
    {
        return new CodeStarConnectionsSourceAction(_props);
    }

    /// <summary>
    /// Configures the ARN of the code star connection used for this source.
    /// </summary>
    /// <param name="arn"></param>
    /// <returns></returns>
    public CodeStarFactory UsesConnection(string arn)
    {
        _props.ConnectionArn = arn;

        return this;
    }

    /// <summary>
    /// Configures the used repository owner and name.
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="repo"></param>
    /// <returns></returns>
    public CodeStarFactory FromRepository(string owner, string repo)
    {
        _props.Owner = owner;
        _props.Repo = repo;

        return this;
    }

    /// <summary>
    /// Sets the used branch of the repository.
    /// </summary>
    /// <param name="branch"></param>
    /// <param name="triggerOnPush"></param>
    /// <returns></returns>
    public CodeStarFactory UseBranch(string branch, bool triggerOnPush = true)
    {
        _props.Branch = branch;
        _props.TriggerOnPush = triggerOnPush;

        return this;
    }

    /// <summary>
    /// Configures the output artifact of the source.
    /// </summary>
    /// <param name="outputName"></param>
    /// <returns></returns>
    public CodeStarFactory HasOutput(string outputName)
    {
        _props.Output = _builder.UseArtifact(outputName);
        return this;
    }
}