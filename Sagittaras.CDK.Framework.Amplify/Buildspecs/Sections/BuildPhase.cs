namespace Sagittaras.CDK.Framework.Amplify.Buildspecs.Sections;

/// <summary>
/// Describes a single phase of the BuildSpec file.
/// </summary>
public class BuildPhase
{
    /// <summary>
    /// List of commands for the phase.
    /// </summary>
    public List<string> Commands { get; set; } = new();

    /// <summary>
    /// Adds a command to the phase.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public BuildPhase Command(string command)
    {
        Commands.Add(command);
        return this;
    }

    /// <summary>
    /// Converts the phase definition to the dictionary object suitable from BuildSpec serialization in CDK.
    /// </summary>
    /// <returns></returns>
    public IDictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> dictionary = new()
        {
            { "commands", Commands.ToArray() }
        };

        return dictionary;
    }
}