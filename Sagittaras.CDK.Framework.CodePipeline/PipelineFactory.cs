using Amazon.CDK.AWS.CodePipeline;
using Amazon.CDK.AWS.S3;
using Constructs;
using Sagittaras.CDK.Framework.CodePipeline.Stages;
using Sagittaras.CDK.Framework.Factory;

namespace Sagittaras.CDK.Framework.CodePipeline;

/// <summary>
/// Factory for creating a CodePipeline.
/// </summary>
public class PipelineFactory : ConstructFactory<Pipeline, PipelineProps>
{
    /// <summary>
    /// List of stages defined for the pipeline.
    /// </summary>
    private readonly List<PipelineStageBuilder> _stageBuilders = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scope"></param>
    /// <param name="pipelineName"></param>
    /// <param name="artifactBucket"></param>
    public PipelineFactory(Construct scope, string pipelineName, string artifactBucket) : base(scope, pipelineName)
    {
        Props = new PipelineProps
        {
            PipelineName = Cloudspace.ResourceName(pipelineName),
            RestartExecutionOnUpdate = true,
            ArtifactBucket = Bucket.FromBucketName(this, "artifact-bucket", artifactBucket)
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scope"></param>
    /// <param name="pipelineName"></param>
    /// <param name="artifactBucket"></param>
    public PipelineFactory(Construct scope, string pipelineName, IBucket artifactBucket) : base(scope, pipelineName)
    {
        Props = new PipelineProps
        {
            PipelineName = Cloudspace.ResourceName(pipelineName),
            RestartExecutionOnUpdate = true,
            ArtifactBucket = artifactBucket
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scope"></param>
    /// <param name="pipelineName"></param>
    public PipelineFactory(Construct scope, string pipelineName) : base(scope, pipelineName)
    {
        Props = new PipelineProps
        {
            PipelineName = Cloudspace.ResourceName(pipelineName),
            RestartExecutionOnUpdate = true
        };
    }

    /// <summary>
    /// Props used to define the pipeline.
    /// </summary>
    public override PipelineProps Props { get; }

    /// <summary>
    /// Defines artifacts used within the pipeline.
    /// </summary>
    public Dictionary<string, Artifact_> Artifacts { get; } = new();

    /// <summary>
    ///     Access to the stage builders defined for the pipeline.
    /// </summary>
    public IReadOnlyCollection<PipelineStageBuilder> Stages => _stageBuilders.AsReadOnly();

    /// <inheritdoc />
    public override Pipeline Construct()
    {
        Props.Stages = _stageBuilders
            .Select(x => x.Construct())
            .ToArray();

        return new Pipeline(this, "pipeline", Props);
    }

    /// <summary>
    /// Creates a new stage from basic builder.
    /// </summary>
    /// <param name="name">Name of the stage.</param>
    /// <returns></returns>
    public PipelineStageBuilder HasStage(string name)
    {
        PipelineStageBuilder? builder = _stageBuilders.FirstOrDefault(x => x.StageName == name);
        if (builder is not null)
        {
            return builder;
        }

        builder = new PipelineStageBuilder(this, name);
        _stageBuilders.Add(builder);

        return builder;
    }

    /// <summary>
    /// Define the artifact with the given name. 
    /// </summary>
    /// <param name="name"></param>
    /// <returns>Existing artifact with the given name or newly created one.</returns>
    public Artifact_ HasArtifact(string name)
    {
        if (Artifacts.TryGetValue(name, out Artifact_? artifact))
        {
            return artifact;
        }

        artifact = new Artifact_(name);
        Artifacts.Add(name, artifact);

        return artifact;
    }
}