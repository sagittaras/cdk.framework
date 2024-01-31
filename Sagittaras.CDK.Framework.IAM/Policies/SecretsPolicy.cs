using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.SecretsManager;

namespace Sagittaras.CDK.Framework.IAM.Policies;

/// <summary>
/// Policy about accessing the Secrets Manager.
/// </summary>
public static class SecretsPolicy
{
    /// <summary>
    /// Creates a policy statements that allows reading of passed secrets.
    /// </summary>
    /// <param name="secrets"></param>
    /// <returns></returns>
    public static PolicyStatement AllowReadSecrets(params ISecret[] secrets)
    {
        return new PolicyStatement(new PolicyStatementProps
        {
            Effect = Effect.ALLOW,
            Actions = new[]
            {
                "secretsmanager:DescribeSecret",
                "secretsmanager:GetSecretValue"
            },
            Resources = secrets.Select(x => x.SecretArn).ToArray()
        });
    }
}