using Amazon.CDK.AWS.CodeBuild;
using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;

namespace Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Factory;

public abstract class BuildSpecFactory : IBuildSpecFactory
{
    /// <summary>
    /// Contains instances of currently described sections.
    /// </summary>
    /// <remarks>
    /// If the section wasn't described, it's not included in the dictionary and thus not included in the
    /// final BuildSpec.
    /// </remarks>
    private readonly Dictionary<Type, IBuildSpecSection> _describedSections = new();

    /// <summary>
    /// Contains types of sections that can be described by the build spec.
    /// </summary>
    /// <remarks>
    /// Works like a factory for the sections.
    /// </remarks>
    private readonly Dictionary<Type, Type> _availableSections = new();

    /// <summary>
    /// Number of build spec's version.
    /// </summary>
    private double _version = 0;

    public IBuildSpecFactory Version(double version)
    {
        _version = version;
        return this;
    }

    /// <inheritdoc />
    public BuildSpec ToBuildSpecYaml()
    {
        return BuildSpec.FromObjectToYaml(ToDictionary());
    }

    /// <inheritdoc />
    public BuildSpec ToBuildSpecJson()
    {
        return BuildSpec.FromObject(ToDictionary());
    }

    /// <summary>
    /// Registers a new section that is available under the specified type.
    /// </summary>
    /// <typeparam name="TImplementation"></typeparam>
    /// <typeparam name="TRealization"></typeparam>
    protected void RegisterSection<TImplementation, TRealization>()
        where TImplementation : IBuildSpecSection
        where TRealization : class, TImplementation, new()
    {
        _availableSections[typeof(TImplementation)] = typeof(TRealization);
    }

    /// <summary>
    /// Gets instance of the section. If not available, it's created.
    /// </summary>
    /// <typeparam name="TSection"></typeparam>
    /// <returns></returns>
    protected TSection GetRequiredSection<TSection>()
        where TSection : IBuildSpecSection
    {
        Type type = typeof(TSection);

        if (_describedSections.TryGetValue(type, out IBuildSpecSection? section))
        {
            return (TSection)section;
        }

        if (!_availableSections.TryGetValue(type, out Type? realization))
        {
            throw new InvalidOperationException($"The section {type.Name} is not available.");
        }

        section = (TSection)Activator.CreateInstance(realization)!;
        _describedSections[type] = section;

        return (TSection)section;
    }

    /// <summary>
    /// Tries to request the section of the BuildSpec for further definition.
    /// </summary>
    /// <typeparam name="TSection"></typeparam>
    /// <returns></returns>
    protected TSection? GetSection<TSection>()
        where TSection : IBuildSpecSection
    {
        Type sectionType = typeof(TSection);

        if (_describedSections.TryGetValue(sectionType, out IBuildSpecSection? section))
        {
            return (TSection)section;
        }

        return default;
    }

    /// <summary>
    /// Converts the factory definitions to a dictionary.
    /// </summary>
    /// <returns></returns>
    private IDictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> buildSpec = new()
        {
            { "version", _version }
        };

        foreach (IBuildSpecSection section in _describedSections.Values)
        {
            buildSpec.Add(section.SectionName, section.ToDictionary());
        }

        return buildSpec;
    }
}