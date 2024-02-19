using Amazon.CDK.AWS.CodeBuild;
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
    private readonly Dictionary<BuildPhase, IBuildPhaseSection> _describedPhases = new();

    /// <summary>
    /// Translations of the enum values to the section names.
    /// </summary>
    private readonly Dictionary<BuildPhase, string> _phaseSectionName = new()
    {
        { BuildPhase.Install, "install" },
        { BuildPhase.PreBuild, "pre_build" },
        { BuildPhase.Build, "build" },
        { BuildPhase.PostBuild, "post_build" }
    };

    /// <inheritdoc />
    public string SectionName => "phases";

    /// <summary>
    /// Rewrites the default name mapping of the section.
    /// </summary>
    /// <param name="phase"></param>
    /// <param name="name"></param>
    protected void RewriteSectionName(BuildPhase phase, string name)
    {
        _phaseSectionName[phase] = name;
    }

    /// <inheritdoc />
    public IDictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> dict = new();
        foreach (IBuildPhaseSection section in _describedPhases.Values)
        {
            dict.Add(section.SectionName, section.ToDictionary());
        }

        return dict;
    }

    /// <inheritdoc />
    public virtual IBuildPhaseSection Phase(BuildPhase phase)
    {
        if (_describedPhases.TryGetValue(phase, out IBuildPhaseSection? section))
        {
            return section;
        }

        section = new BuildPhaseSection(_phaseSectionName[phase]);
        _describedPhases.Add(phase, section);

        return section;
    }
}