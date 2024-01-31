using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;

namespace Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Factory.Sections;

/// <summary>
/// Manages the phases section.
/// </summary>
public class PhasesSection : IPhasesSection
{
    /// <summary>
    /// Dictionary of defined phases.
    /// </summary>
    private readonly Dictionary<BuildPhase, IBuildPhaseSection> _phases = new();

    /// <summary>
    /// Translations of the enum values to the section names.
    /// </summary>
    private readonly Dictionary<BuildPhase, string> _phaseSectionName = new()
    {
        { Abstraction.BuildPhase.Install, "install" },
        { Abstraction.BuildPhase.PreBuild, "pre_build" },
        { Abstraction.BuildPhase.Build, "build" },
        { Abstraction.BuildPhase.PostBuild, "post_build" }
    };

    /// <inheritdoc />
    public string SectionName => "phases";

    /// <inheritdoc />
    public IDictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> dict = new();
        foreach (IBuildPhaseSection section in _phases.Values)
        {
            dict.Add(section.SectionName, section.ToDictionary());
        }

        return dict;
    }

    /// <inheritdoc />
    public IBuildPhaseSection BuildPhase(BuildPhase phase)
    {
        if (_phases.TryGetValue(phase, out IBuildPhaseSection? section))
        {
            return section;
        }

        section = new BuildPhaseSection(_phaseSectionName[phase]);
        _phases.Add(phase, section);

        return section;
    }
}