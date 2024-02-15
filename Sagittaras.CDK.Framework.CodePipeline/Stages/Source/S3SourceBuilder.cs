using Amazon.CDK.AWS.CodePipeline.Actions;
using Amazon.CDK.AWS.S3;

namespace Sagittaras.CDK.Framework.CodePipeline.Stages.Source;

/// <summary>
/// Builds the source action which is using S3 bucket.
/// </summary>
public class S3SourceBuilder : ActionBuilder<S3SourceAction>
{
    private readonly PipelineStageBuilder _builder;
    private readonly S3SourceActionProps _props;

    public S3SourceBuilder(PipelineStageBuilder builder, string name) : base(builder, name)
    {
        _builder = builder;
        _props = new S3SourceActionProps
        {
            ActionName = name
        };
    }

    /// <inheritdoc />
    public override S3SourceAction Construct()
    {
        return new S3SourceAction(_props);
    }

    /// <summary>
    /// Uses the bucket and the object key to define the source action.
    /// </summary>
    /// <remarks>
    /// A bucket reference is created from the bucket name.
    /// </remarks>
    /// <param name="bucketName"></param>
    /// <param name="objectKey"></param>
    /// <returns></returns>
    public S3SourceBuilder FromBucket(string bucketName, string objectKey)
    {
        _props.Bucket = Bucket.FromBucketName(_builder, "source-bucket", bucketName);
        _props.BucketKey = objectKey;
        return this;
    }

    /// <summary>
    /// Uses the bucket and the object key to define the source action.
    /// </summary>
    /// <remarks>
    /// A bucket reference is created from the bucket name.
    /// </remarks>
    /// <param name="bucketArn"></param>
    /// <param name="objectKey"></param>
    /// <returns></returns>
    public S3SourceBuilder FromBucketArn(string bucketArn, string objectKey)
    {
        _props.Bucket = Bucket.FromBucketArn(_builder, "source-bucket", bucketArn);
        _props.BucketKey = objectKey;
        return this;
    }

    /// <summary>
    /// Uses the bucket and the object key to define the source action.
    /// </summary>
    /// <param name="bucket"></param>
    /// <param name="objectKey"></param>
    /// <returns></returns>
    public S3SourceBuilder FromBucket(IBucket bucket, string objectKey)
    {
        _props.Bucket = bucket;
        _props.BucketKey = objectKey;
        return this;
    }

    /// <summary>
    /// Specifies the output artifact for the source action.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public S3SourceBuilder HasOutput(string name)
    {
        _props.Output = UseArtifact(name);
        return this;
    }

    /// <summary>
    /// What kind of trigger is used for the S3 source action.
    /// </summary>
    /// <param name="trigger"></param>
    /// <returns></returns>
    public S3SourceBuilder UseTrigger(S3Trigger trigger)
    {
        _props.Trigger = trigger;
        return this;
    }
}