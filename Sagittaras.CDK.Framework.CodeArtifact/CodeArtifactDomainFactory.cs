using Amazon.CDK.AWS.CodeArtifact;
using Constructs;
using Sagittaras.CDK.Framework.Extensions;
using Sagittaras.CDK.Framework.Factory;

namespace Sagittaras.CDK.Framework.CodeArtifact;

/// <summary>
/// Factory used for building CodeArtifact domain & their repositories.
/// </summary>
public class CodeArtifactDomainFactory : ConstructFactory<CfnDomain, CfnDomainProps>
{
    /// <summary>
    /// List with all available repositories.
    /// </summary>
    private readonly List<CfnRepositoryProps> _repositories = new();

    public CodeArtifactDomainFactory(Construct scope, string domainName) : base(scope, domainName)
    {
        Props = new CfnDomainProps
        {
            DomainName = domainName
        };
    }

    /// <summary>
    /// Props describing the domain.
    /// </summary>
    public override CfnDomainProps Props { get; }

    /// <inheritdoc />
    public override CfnDomain Construct()
    {
        CfnDomain domain = new(this, "domain", Props);
        _ = _repositories.Select(x => new CfnRepository(this, $"repository-{x.RepositoryName.ToResourceId()}", x));

        return domain;
    }

    /// <summary>
    /// Adds a new repository to the domain.
    /// </summary>
    /// <param name="repositoryName">Name of the repository.</param>
    /// <param name="description">Description for the repository.</param>
    /// <returns></returns>
    public CodeArtifactDomainFactory AddRepository(string repositoryName, string? description = null)
    {
        _repositories.Add(new CfnRepositoryProps
        {
            DomainName = Props.DomainName,
            RepositoryName = repositoryName,
            Description = description
        });
        return this;
    }
}