using Amazon.CDK.AWS.CodePipeline;
using Sagittaras.CDK.Framework.Factory;

namespace Sagittaras.CDK.Framework.CodePipeline.Stages;

public abstract class ActionBuilder<TAction> : CdkFactory<TAction>, IActionBuilder
    where TAction : IAction
{
    private readonly PipelineStageBuilder _builder;

    protected ActionBuilder(PipelineStageBuilder builder, string name) : base(builder, name)
    {
        _builder = builder;
        ActionName = name;
    }

    /// <inheritdoc />
    public string ActionName { get; }

    /// <inheritdoc />
    public abstract override TAction Construct();

    /// <inheritdoc />
    IAction ICdkFactory<IAction>.Construct()
    {
        return Construct();
    }
    
    /// <inheritdoc />
    public abstract IActionBuilder RunOrder(int order);

    /// <summary>
    ///     Uses an artifact with the given name.
    /// </summary>
    /// <param name="artifactName"></param>
    /// <returns></returns>
    protected Artifact_ UseArtifact(string artifactName)
    {
        return _builder.UseArtifact(artifactName);
    }
}