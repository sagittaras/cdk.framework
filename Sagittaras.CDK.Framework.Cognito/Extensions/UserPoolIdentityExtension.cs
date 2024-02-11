using Amazon.CDK;
using Amazon.CDK.AWS.Cognito;
using Sagittaras.CDK.Framework.Extensions;

namespace Sagittaras.CDK.Framework.Cognito.Extensions;

/// <summary>
///     Extension methods that allows for easier configuration of the User Pool Identity Providers.
/// </summary>
public static class UserPoolIdentityExtension
{
    /// <summary>
    ///     Adds a Google provider to the User Pool.
    /// </summary>
    /// <param name="pool">Instance of the User Pool.</param>
    /// <param name="clientId">Google provided ID.</param>
    /// <param name="secretArn">ARN of value in Secrets Manger containing the Google provided Secret ID.</param>
    /// <param name="configure">Allow for additional configuration of the props.</param>
    public static void AddGoogleProvider(this UserPool pool, string clientId, string secretArn, Action<UserPoolIdentityProviderGoogleProps>? configure = null)
    {
        UserPoolIdentityProviderGoogleProps props = new()
        {
            UserPool = pool,
            ClientId = clientId,
            ClientSecretValue = SecretValue.SecretsManager(secretArn)
        };
        configure?.Invoke(props);
        _ = new UserPoolIdentityProviderGoogle(pool, clientId.ToResourceId(), props);
    }
}