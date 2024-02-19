using Sagittaras.CDK.Framework.Amplify.BuildSpecification.Abstraction;
using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;

namespace Sagittaras.CDK.Framework.Amplify.BuildSpecification.Factory;

/// <summary>
/// Describes the tests of amplify application.
/// </summary>
public class TestSection : ITestSection
{
    private AmplifyArtifactsSection? _artifacts;

    /// <summary>
    /// Dictionary of phases that are described.
    /// </summary>
    private readonly Dictionary<TestPhase, ITestPhaseSection> _describedPhases = new();

    /// <summary>
    /// Translations of the enum values to the section names.
    /// </summary>
    private readonly Dictionary<TestPhase, string> _phaseSectionName = new()
    {
        { TestPhase.PreTest, "preTest" },
        { TestPhase.Test, "test" },
        { TestPhase.PostTest, "postTest" }
    };

    /// <inheritdoc />
    public string SectionName => "tes";

    /// <inheritdoc />
    public IArtifactsSection Artifacts => _artifacts ??= new AmplifyArtifactsSection();

    /// <inheritdoc />
    public IDictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> dict = new()
        {
            { "phases", PhasesDictionary() }
        };

        if (_artifacts is not null)
        {
            dict.Add(_artifacts.SectionName, _artifacts.ToDictionary());
        }

        return dict;
    }

    /// <inheritdoc />
    public ITestPhaseSection Phase(TestPhase phase)
    {
        if (_describedPhases.TryGetValue(phase, out ITestPhaseSection? section))
        {
            return section;
        }

        section = new TestPhaseSection(_phaseSectionName[phase]);
        _describedPhases.Add(phase, section);

        return section;
    }

    /// <summary>
    /// Converts the phases to the dictionary.
    /// </summary>
    /// <returns></returns>
    private IDictionary<string, object> PhasesDictionary()
    {
        Dictionary<string, object> dict = new();
        foreach (ITestPhaseSection section in _describedPhases.Values)
        {
            dict.Add(section.SectionName, section.ToDictionary());
        }

        return dict;
    }
}