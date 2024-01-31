using Amazon.CDK.AWS.IAM;

namespace Sagittaras.CDK.Framework.IAM.Extensions;

/// <summary>
/// Useful extension of the user CDK class.
/// </summary>
public static class UserExtension
{
    /// <summary>
    /// Allow the user to list secrets in secrets manager.
    /// </summary>
    /// <param name="user"></param>
    public static void AllowSecretsListing(this User user)
    {
        user.AddToPolicy(new PolicyStatement(new PolicyStatementProps
        {
            Sid = "ListSecrets",
            Effect = Effect.ALLOW,
            Actions = new[] { "secretsmanager:ListSecrets" },
            Resources = new[] { "*" }
        }));
    }
}