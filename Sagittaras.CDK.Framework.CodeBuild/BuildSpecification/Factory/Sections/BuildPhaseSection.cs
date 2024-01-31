using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;

namespace Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Factory.Sections;

/// <summary>
/// Describes a specific build phase section.
/// </summary>
public class BuildPhaseSection : IBuildPhaseSection
{
    /// <summary>
    /// List of commands for this section.
    /// </summary>
    private readonly List<string> _commands = new();

    /// <summary>
    /// List of finally commands of this section, that are executed even if the section fails.
    /// </summary>
    private readonly List<string> _finally = new();

    /// <summary>
    /// Behaviour on failure of the section.
    /// </summary>
    private FailureBehaviour _failureBehaviour = FailureBehaviour.Continue;

    public BuildPhaseSection(string sectionName)
    {
        SectionName = sectionName;
    }

    /// <inheritdoc />
    public string SectionName { get; }

    /// <inheritdoc />
    public IDictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> dict = new()
        {
            { "on-failure", _failureBehaviour.ToString().ToUpper() },
            { "commands", _commands.ToArray() }
        };

        if (_finally.Count > 0)
        {
            dict.Add("finally", _finally.ToArray());
        }

        return dict;
    }

    /// <inheritdoc />
    public IBuildPhaseSection OnFailure(FailureBehaviour failureBehaviour)
    {
        _failureBehaviour = failureBehaviour;
        return this;
    }

    /// <inheritdoc />
    public IBuildPhaseSection Command(string command)
    {
        _commands.Add(command);
        return this;
    }

    /// <inheritdoc />
    public IBuildPhaseSection Finally(string command)
    {
        _finally.Add(command);
        return this;
    }
}