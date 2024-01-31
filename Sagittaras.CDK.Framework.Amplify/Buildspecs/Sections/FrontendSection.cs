namespace Sagittaras.CDK.Framework.Amplify.Buildspecs.Sections;

/// <summary>
/// Sections defining the frontend resources.
/// </summary>
public class FrontendSection
{
    /// <summary>
    /// Definition of the phases.
    /// </summary>
    private readonly Dictionary<string, BuildPhase> _phases = new();

    /// <summary>
    /// Translates enum values to the correct string.
    /// </summary>
    private readonly Dictionary<PhaseType, string> _phaseTranslation = new()
    {
        { PhaseType.PreBuild, "preBuild" },
        { PhaseType.Build, "build" },
        { PhaseType.PostBuild, "postBuild" }
    };

    /// <summary>
    /// Frontend artifacts section.
    /// </summary>
    private ArtifactsSection? _artifactsSection;

    /// <summary>
    /// Frontend caching options.
    /// </summary>
    private CacheSection? _cacheSection;

    /// <summary>
    /// Gets configuration for selected phase.
    /// </summary>
    /// <param name="phaseType"></param>
    /// <returns></returns>
    public BuildPhase Phase(PhaseType phaseType)
    {
        string translated = _phaseTranslation[phaseType];

        if (_phases.TryGetValue(translated, out BuildPhase? phase))
        {
            return phase;
        }

        phase = new BuildPhase();
        _phases.Add(translated, phase);

        return phase;
    }

    /// <summary>
    /// Gets the configuration of artifacts.
    /// </summary>
    /// <returns></returns>
    public ArtifactsSection Artifacts()
    {
        return _artifactsSection ??= new ArtifactsSection();
    }

    /// <summary>
    /// Gets the configuration of caching options.
    /// </summary>
    /// <returns></returns>
    public CacheSection Cache()
    {
        return _cacheSection ??= new CacheSection();
    }

    /// <summary>
    /// Converts the section to dictionary.
    /// </summary>
    /// <returns></returns>
    public IDictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> dict = new();

        Dictionary<string, object> phases = new();
        foreach ((string name, BuildPhase phase) in _phases)
        {
            phases.Add(name, phase.ToDictionary());
        }
        dict.Add("phases", phases);

        if (_artifactsSection is not null)
        {
            dict.Add("artifacts", _artifactsSection.ToDictionary());
        }

        if (_cacheSection is not null)
        {
            dict.Add("cache", _cacheSection.ToDictionary());
        }

        return dict;
    }
}