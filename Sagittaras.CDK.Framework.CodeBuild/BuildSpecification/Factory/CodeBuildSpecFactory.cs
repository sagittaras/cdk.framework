using Amazon.CDK.AWS.CodeBuild;
using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;
using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Factory.Sections;
using Sagittaras.CDK.Framework.CodeBuild.Extensions;

namespace Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Factory;

/// <summary>
/// Implementation of <see cref="IBuildSpecSection"/> allowing common definition of the BuildSpec.
/// </summary>
public class CodeBuildSpecFactory : BuildSpecFactory, ICodeBuildSpecFactory
{
    public CodeBuildSpecFactory()
    {
        Version(0.2);
        
        RegisterSection<IEnvironmentSection, EnvironmentSection>();
        RegisterSection<IPhasesSection, PhasesSection>();
        RegisterSection<IArtifactsSection, ArtifactsSection>();
        RegisterSection<ICacheSection, CacheSection>();
    }

    /// <inheritdoc />
    public IEnvironmentSection Environment => GetRequiredSection<IEnvironmentSection>();

    /// <inheritdoc />
    public virtual IPhasesSection Phases => GetRequiredSection<IPhasesSection>();

    /// <inheritdoc />
    public IArtifactsSection Artifacts => GetRequiredSection<IArtifactsSection>();

    /// <inheritdoc />
    public ICacheSection Cache => GetRequiredSection<ICacheSection>();
    
    /// <inheritdoc />
    public virtual void ConfigureProjectPolicies(IProject project)
    {
        // If the project has environment section, allow read to all secrets defined in the section.
        if (GetSection<IEnvironmentSection>() is { } env && env.Secrets.Any())
        {
            project.AddSecretsAccess(env.Secrets.ToArray());
        }
    }
}