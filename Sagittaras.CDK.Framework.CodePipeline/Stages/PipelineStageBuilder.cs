using Amazon.CDK.AWS.CodePipeline;
using Sagittaras.CDK.Framework.CodePipeline.Stages.Abstractions;
using Sagittaras.CDK.Framework.Factory;

namespace Sagittaras.CDK.Framework.CodePipeline.Stages;

/// <summary>
/// Basic implementation of a pipeline stage builder.
/// </summary>
public class PipelineStageBuilder : CdkFactory<IStageProps>, IPipelineStageBuilder
{
    /// <summary>
    /// Factory in which context the stage is being created.
    /// </summary>
    private readonly PipelineFactory _factory;

    /// <summary>
    /// Props to be used for creating the stage.
    /// </summary>
    private readonly StageProps _props;

    /// <summary>
    /// List of actions defined for the stage.
    /// </summary>
    private readonly List<IActionBuilder> _actions = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory">Pipeline factory in which context the stage is being created.</param>
    /// <param name="stageName">Name of the stage.</param>
    public PipelineStageBuilder(PipelineFactory factory, string stageName) : base(factory, stageName)
    {
        _factory = factory;
        _props = new StageProps
        {
            StageName = stageName
        };
    }

    /// <summary>
    /// Gets the name of the stage.
    /// </summary>
    public string StageName => _props.StageName;

    /// <inheritdoc />
    public override IStageProps Construct()
    {
        _props.Actions = _actions
            .Select(x => x.Construct())
            .ToArray();
        
        return _props;
    }

    /// <inheritdoc />
    public IPipelineStageBuilder AddAction(IActionBuilder action)
    {
        _actions.Add(action);
        return this;
    }

    /// <inheritdoc />
    public IActionBuilder GetAction(string name)
    {
        return _actions.First(x => x.ActionName == name);
    }

    /// <summary>
    /// Uses an artifact with the given name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Artifact_ UseArtifact(string name)
    {
        return _factory.HasArtifact(name);
    }
}