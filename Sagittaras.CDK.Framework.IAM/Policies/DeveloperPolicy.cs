using Amazon.CDK.AWS.IAM;

namespace Sagittaras.CDK.Framework.IAM.Policies;

/// <summary>
/// Policy statement that allows access to the developer tools.
/// </summary>
public static class DeveloperPolicy
{
    /// <summary>
    /// Allows to get the CodeArtifact authorization token.
    /// </summary>
    public static PolicyStatement CodeArtifactAuthorization => new(new PolicyStatementProps
    {
        Sid = "CodeArtifactAuthorization",
        Effect = Effect.ALLOW,
        Actions = new[] { "sts:GetServiceBearerToken" },
        Resources = new[] { "*" },
        Conditions = new Dictionary<string, object>
        {
            {
                "StringEquals", new Dictionary<string, object>
                {
                    { "sts:AWSServiceName", "codeartifact.amazonaws.com" }
                }
            }
        }
    });

    /// <summary>
    /// Allows to read from the CodeArtifact repositories.
    /// </summary>
    public static PolicyStatement CodeArtifactReadOnly => new(new PolicyStatementProps
    {
        Sid = "CodeArtifactReadOnly",
        Effect = Effect.ALLOW,
        Actions = new[]
        {
            "codeartifact:Describe*",
            "codeartifact:Get*",
            "codeartifact:List*",
            "codeartifact:ReadFromRepository"
        },
        Resources = new[] { "*" }
    });

    /// <summary>
    /// Allows to publish to the CodeArtifact repositories.
    /// </summary>
    public static PolicyStatement CodeArtifactPublish => new(new PolicyStatementProps
    {
        Sid = "CodeArtifactPublish",
        Effect = Effect.ALLOW,
        Actions = new[]
        {
            "codeartifact:GetAuthorizationToken",
            "codeartifact:GetRepositoryEndpoint",
            "codeartifact:ReadFromRepository",
            "codeartifact:PublishPackageVersion"
        },
        Resources = new[] { "*" }
    });
}