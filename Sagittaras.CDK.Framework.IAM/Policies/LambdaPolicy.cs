using Amazon.CDK.AWS.IAM;

namespace Sagittaras.CDK.Framework.IAM.Policies;

/// <summary>
/// Common policies for the Lambda functions.
/// </summary>
public static class LambdaPolicy
{
    /// <summary>
    /// Allows to request layer version information.
    /// </summary>
    public static PolicyStatement GetLayerVersion => new(new PolicyStatementProps
    {
        Sid = "GetLayerVersion",
        Effect = Effect.ALLOW,
        Actions = new[] { "lambda:GetLayerVersion" },
        Resources = new[] { "arn:aws:lambda:*:*:layer:AWS-AppConfig-Extension:*" }
    });
}