using Amazon.CDK.AWS.Amplify.Alpha;

namespace Sagittaras.CDK.Framework.Amplify;

/// <summary>
/// Factory for constructing the domains of Amplify Apps.
/// </summary>
public class DomainFactory
{
    /// <summary>
    /// Options for newly constructed domain.
    /// </summary>
    private readonly DomainOptions _options;

    /// <summary>
    /// Dictionary of sub domains for the domain.
    /// </summary>
    private readonly Dictionary<string, string> _subDomains = new();

    public DomainFactory(string domainName)
    {
        DomainName = domainName;
        _options = new DomainOptions
        {
            DomainName = domainName
        };
    }

    /// <summary>
    /// Name of the domain.
    /// </summary>
    public string DomainName { get; }

    /// <summary>
    /// Construct the options for creating the domain in the amplify app.
    /// </summary>
    /// <returns></returns>
    public DomainOptions Construct(IDictionary<string, Branch> branches)
    {
        List<ISubDomain> createdSubDomains = new();
        foreach ((string subDomain, string branchName) in _subDomains)
        {
            if (!branches.TryGetValue(branchName, out Branch? branch))
            {
                continue;
            }

            createdSubDomains.Add(new SubDomain
            {
                Branch = branch,
                Prefix = subDomain
            });
        }

        _options.SubDomains = createdSubDomains.ToArray();

        return _options;
    }

    /// <summary>
    /// Enables auto-creation of sub domains from the existing branches.
    /// </summary>
    /// <param name="patterns"></param>
    /// <returns></returns>
    public DomainFactory AutoSubDomainCreation(string[] patterns)
    {
        _options.EnableAutoSubdomain = true;
        _options.AutoSubdomainCreationPatterns = patterns;
        return this;
    }

    /// <summary>
    /// Adds a new subdomain to the domain.
    /// </summary>
    /// <param name="subDomain"></param>
    /// <param name="branch"></param>
    /// <returns></returns>
    public DomainFactory AddSubDomain(string subDomain, string branch)
    {
        _subDomains.Add(subDomain, branch);
        return this;
    }
}