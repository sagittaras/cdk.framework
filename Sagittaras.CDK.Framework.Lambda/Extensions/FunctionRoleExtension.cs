using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;

namespace Sagittaras.CDK.Framework.Lambda.Extensions;

/// <summary>
///     Useful extension to the role of the function.
/// </summary>
public static class FunctionRoleExtension
{
    /// <summary>
    ///     Allows the function to access AppConfig.
    /// </summary>
    /// <param name="function"></param>
    public static void AllowAppConfig(this Function function)
    {
        function.AddToRolePolicy(new PolicyStatement(new PolicyStatementProps
        {
            Sid = "AppConfigAccess",
            Effect = Effect.ALLOW,
            Actions = new[]
            {
                "appconfig:GetLatestConfiguration",
                "appconfig:StartConfigurationSession"
            },
            Resources = new[] { $"arn:aws:appconfig:*:{Cloudspace.AccountId}:application/*/environment/*/configuration/*" }
        }));
    }

    /// <summary>
    ///     Allows the function to send messages to the specified SQS queue.
    /// </summary>
    /// <param name="function"></param>
    /// <param name="queueArn">ARN of the Queue. If not set, all queues under the account ID are allowed.</param>
    public static void AllowSendMessageToSqs(this Function function, string? queueArn = null)
    {
        function.AddToRolePolicy(new PolicyStatement(new PolicyStatementProps
        {
            Effect = Effect.ALLOW,
            Resources = new[]
            {
                queueArn ?? $"arn:aws:sqs:eu-central-1:{Cloudspace.AccountId}:*"
            },
            Actions = new[]
            {
                "sqs:sendmessage"
            }
        }));
    }

    /// <summary>
    ///     Allows the function to access S3 storage.
    /// </summary>
    /// <param name="function"></param>
    /// <param name="bucketName">Name of the bucket. If not set, all buckets are allowed.</param>
    public static void AllowS3Access(this Function function, string? bucketName)
    {
        function.AddToRolePolicy(new PolicyStatement(new PolicyStatementProps
        {
            Effect = Effect.ALLOW,
            Resources = new[]
            {
                bucketName == null ? "arn:aws:s3:::*" : $"arn:aws:s3:::{bucketName}"
            },
            Actions = new[]
            {
                "s3:*",
                "s3-object-lambda:*"
            }
        }));
    }
}