using Amazon.CDK.AWS.CodePipeline.Actions;
using Amazon.CDK.AWS.S3;

namespace Sagittaras.CDK.Framework.CodePipeline.Stages.Deploy;

/// <summary>
/// Builds a action that deploys the artifacts to the S3 bucket.
/// </summary>
public class S3DeployBuilder : ActionBuilder<S3DeployAction>
{
    private readonly PipelineStageBuilder _builder;
    private readonly S3DeployActionProps _props;

    public S3DeployBuilder(PipelineStageBuilder builder, string name) : base(builder, name)
    {
        _builder = builder;
        _props = new S3DeployActionProps
        {
            ActionName = name
        };
    }

    /// <inheritdoc />
    public override S3DeployAction Construct()
    {
        return new S3DeployAction(_props);
    }

    /// <summary>
    /// Sets the used input artifact.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public S3DeployBuilder UsesInput(string input)
    {
        _props.Input = UseArtifact(input);
        return this;
    }

    /// <summary>
    /// Sets the target bucket and the object key.
    /// </summary>
    /// <remarks>
    /// Bucket reference is created from the bucket name.
    /// </remarks>
    /// <param name="bucketName"></param>
    /// <param name="objectKey"></param>
    /// <returns></returns>
    public S3DeployBuilder ToBucket(string bucketName, string objectKey)
    {
        _props.Bucket = Bucket.FromBucketName(_builder, "deploy-bucket", bucketName);
        _props.ObjectKey = objectKey;
        return this;
    }

    /// <summary>
    /// Sets the target bucket and the object key.
    /// </summary>
    /// <remarks>
    /// Bucket reference is created from the bucket name.
    /// </remarks>
    /// <param name="bucketArn"></param>
    /// <param name="objectKey"></param>
    /// <returns></returns>
    public S3DeployBuilder ToBucketArn(string bucketArn, string objectKey)
    {
        _props.Bucket = Bucket.FromBucketArn(_builder, "deploy-bucket", bucketArn);
        _props.ObjectKey = objectKey;
        return this;
    }

    /// <summary>
    /// Sets the target bucket and the object key.
    /// </summary>
    /// <param name="bucket"></param>
    /// <param name="objectKey"></param>
    /// <returns></returns>
    public S3DeployBuilder ToBucket(IBucket bucket, string objectKey)
    {
        _props.Bucket = bucket;
        _props.ObjectKey = objectKey;
        return this;
    }

    /// <summary>
    /// Sets that the artifact should be extracted before deploying.
    /// </summary>
    /// <returns></returns>
    public S3DeployBuilder ExtractArtifact()
    {
        _props.Extract = true;
        return this;
    }
}