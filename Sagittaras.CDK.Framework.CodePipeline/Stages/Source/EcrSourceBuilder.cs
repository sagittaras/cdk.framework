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

    /// <inheritdoc />
    public override IActionBuilder RunOrder(int order)
    {
        _props.RunOrder = order;
        return this;
    }

    /// <summary>
    ///     Reference the ECR repository used as the source by its name.
    /// </summary>
    /// <remarks>
    ///     If the repository is on different account, use <see cref="FromRepositoryAttributes"/> or <see cref="FromRepositoryByArn"/> instead.
    /// </remarks>
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
    ///     Reference the ECR repository used as the source by its ARN.
    /// </summary>
    /// <remarks>
    ///     If the ARN is late-bound value (token), use <see cref="FromRepositoryAttributes"/> instead.
    /// </remarks>
    /// <param name="repositoryArn"></param>
    /// <returns></returns>
    public EcrSourceBuilder FromRepositoryByArn(string repositoryArn)
    {
        _props.Repository = Repository.FromRepositoryArn(_builder, "source-repository", repositoryArn);
        return this;
    }

    /// <summary>
    ///     Reference the ECR repository used as the source by its attributes.
    /// </summary>
    /// <param name="repositoryAttributes"></param>
    /// <returns></returns>
    public EcrSourceBuilder FromRepositoryAttributes(IRepositoryAttributes repositoryAttributes)
    {
        _props.Repository = Repository.FromRepositoryAttributes(_builder, "source-repository", repositoryAttributes);
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