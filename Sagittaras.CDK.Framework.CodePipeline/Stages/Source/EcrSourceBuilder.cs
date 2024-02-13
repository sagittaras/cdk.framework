using Amazon.CDK.AWS.CodePipeline.Actions;
using Amazon.CDK.AWS.ECR;

namespace Sagittaras.CDK.Framework.CodePipeline.Stages.Source;

/// <summary>
///     Builder factory used to define a ECR source.
/// </summary>
public class EcrSourceBuilder : ActionBuilder<EcrSourceAction>
{
    /// <summary>
    ///     Builder in which scope the source is being defined.
    /// </summary>
    private readonly PipelineStageBuilder _builder;

    /// <summary>
    ///     Props used to create the source.
    /// </summary>
    private readonly EcrSourceActionProps _props;

    public EcrSourceBuilder(PipelineStageBuilder builder, string name) : base(builder, name)
    {
        _builder = builder;
        _props = new EcrSourceActionProps
        {
            ActionName = name
        };
    }

    /// <inheritdoc />
    public override EcrSourceAction Construct()
    {
        return new EcrSourceAction(_props);
    }

    /// <summary>
    ///     Reference the ECR repository used as the source by its name.
    /// </summary>
    /// <param name="repositoryName"></param>
    /// <returns></returns>
    public EcrSourceBuilder FromRepository(string repositoryName)
    {
        _props.Repository = Repository.FromRepositoryName(_builder, "source-repository", repositoryName);
        return this;
    }

    /// <summary>
    ///     Sets the instance of repository construct which is used as the source.
    /// </summary>
    /// <param name="repository">ECR repository construct.</param>
    /// <returns></returns>
    public EcrSourceBuilder FromRepository(IRepository repository)
    {
        _props.Repository = repository;
        return this;
    }

    /// <summary>
    ///     Watches the given tag of the repository for changes.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public EcrSourceBuilder WatchesTag(string tag)
    {
        _props.ImageTag = tag;
        return this;
    }

    /// <summary>
    ///     Configure the output artifact of the source stage.
    /// </summary>
    /// <param name="outputName"></param>
    /// <returns></returns>
    public EcrSourceBuilder HasOutput(string outputName)
    {
        _props.Output = _builder.UseArtifact(outputName);
        return this;
    }
}