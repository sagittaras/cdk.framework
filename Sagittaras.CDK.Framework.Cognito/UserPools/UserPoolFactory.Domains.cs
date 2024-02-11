using Amazon.CDK.AWS.CertificateManager;
using Amazon.CDK.AWS.Cognito;
using Sagittaras.CDK.Framework.Extensions;

namespace Sagittaras.CDK.Framework.Cognito.UserPools;

public partial class UserPoolFactory
{
    /// <summary>
    ///     Dictionary containing domains configuration for the user-pool.
    /// </summary>
    private readonly Dictionary<string, UserPoolDomainOptions> _domains = new();

    /// <summary>
    ///     Assign all predefined domains to the user-pool.
    /// </summary>
    /// <param name="pool"></param>
    private void AssignDomainsToPool(IUserPool pool)
    {
        foreach ((string domain, UserPoolDomainOptions options) in _domains)
        {
            pool.AddDomain(domain, options);
        }
    }

    /// <summary>
    ///     Sets custom cognito domain prefix.
    /// </summary>
    /// <param name="prefix">Selected prefix.</param>
    public UserPoolFactory SetCognitoDomainPrefix(string prefix)
    {
        _domains.Add($"cognito-{prefix.ToResourceId()}", new UserPoolDomainOptions
        {
            CognitoDomain = new CognitoDomainOptions
            {
                DomainPrefix = prefix
            }
        });
        return this;
    }

    /// <summary>
    ///     Adds a custom domain to the user-pool.
    /// </summary>
    /// <param name="domainName">Name of assigned domain.</param>
    /// <param name="certificateArn">ARN of the certificate for the cognito domain.</param>
    public UserPoolFactory AddCustomDomain(string domainName, string certificateArn)
    {
        _domains.Add(domainName.ToResourceId(), new UserPoolDomainOptions
        {
            CustomDomain = new CustomDomainOptions
            {
                DomainName = domainName,
                Certificate = Certificate.FromCertificateArn(this, $"{domainName.ToResourceId()}-certificate", certificateArn)
            }
        });
        return this;
    }
}