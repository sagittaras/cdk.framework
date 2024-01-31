using Amazon.CDK.AWS.CodeBuild;
using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;
using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Factory.Sections;
using Sagittaras.CDK.Framework.CodeBuild.Extensions;

namespace Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Factory;

/// <summary>
/// Implementation of <see cref="IBuildSpecSection"/> allowing common definition of the BuildSpec.
/// </summary>
public class BuildSpecFactory : IBuildSpecFactory
{
    /// <summary>
    /// Number of version of the buildspec file.
    /// </summary>
    private double _version = 0.2;

    /// <summary>
    /// Dictionary containing common sections of the BuildSpec file.
    /// </summary>
    private readonly Dictionary<Type, IBuildSpecSection> _sections = new();

    /// <inheritdoc />
    public IEnvironmentSection Environment => GetOrCreateSection<IEnvironmentSection>(() => new EnvironmentSection());

    /// <inheritdoc />
    public IPhasesSection Phases => GetOrCreateSection<IPhasesSection>(() => new PhasesSection());

    /// <inheritdoc />
    public IArtifactsSection Artifacts => GetOrCreateSection<IArtifactsSection>(() => new ArtifactsSection());

    /// <inheritdoc />
    public ICacheSection Cache => GetOrCreateSection<ICacheSection>(() => new CacheSection());

    /// <inheritdoc />
    public IBuildSpecFactory Version(double version)
    {
        _version = version;
        return this;
    }

    /// <inheritdoc />
    public BuildSpec ToBuildSpec()
    {
        Dictionary<string, object> buildSpec = new()
        {
            { "version", _version }
        };

        foreach (IBuildSpecSection section in _sections.Values)
        {
            buildSpec.Add(section.SectionName, section.ToDictionary());
        }

        return BuildSpec.FromObjectToYaml(buildSpec);
    }

    /// <inheritdoc />
    public virtual void ConfigureProjectPolicies(IProject project)
    {
        // If the project has environment section, allow read to all secrets defined in the section.
        if (GetSection<IEnvironmentSection>() is { } env && env.Secrets.Any())
        {
            project.AddSecretsAccess(env.Secrets.ToArray());
        }
    }

    /// <summary>
    /// Requests the section of the BuildSpec for further definition.
    /// </summary>
    /// <remarks>
    /// If the section does not exists yet, it will be created.
    /// </remarks>
    /// <param name="createCallback">Callback in which the section class can be created if the section does not exists yet.</param>
    /// <typeparam name="TSection"></typeparam>
    /// <returns></returns>
    private TSection GetOrCreateSection<TSection>(Func<TSection> createCallback)
        where TSection : IBuildSpecSection
    {
        Type sectionType = typeof(TSection);

        if (_sections.TryGetValue(sectionType, out IBuildSpecSection? section))
        {
            return (TSection)section;
        }

        section = createCallback.Invoke();
        _sections.Add(sectionType, section);

        return (TSection)section;
    }

    /// <summary>
    /// Tries to request the section of the BuildSpec for further definition.
    /// </summary>
    /// <typeparam name="TSection"></typeparam>
    /// <returns></returns>
    private TSection? GetSection<TSection>()
        where TSection : IBuildSpecSection
    {
        Type sectionType = typeof(TSection);

        if (_sections.TryGetValue(sectionType, out IBuildSpecSection? section))
        {
            return (TSection)section;
        }

        return default;
    }
}