namespace Sagittaras.CDK.Framework.Amplify.Buildspecs.Sections;

/// <summary>
/// Artifacts section of the BuildSpec file.
/// </summary>
public class ArtifactsSection
{
    /// <summary>
    /// Base directory from which the artifacts are to be uploaded.
    /// </summary>
    public string BaseDirectory { get; set; } = ".";

    /// <summary>
    /// List of files to be uploaded.
    /// </summary>
    public List<string> Files { get; set; } = new();

    /// <summary>
    /// Configures the base directory from which the artifacts are to be uploaded.
    /// </summary>
    /// <param name="baseDirectory"></param>
    /// <returns></returns>
    public ArtifactsSection WithBaseDirectory(string baseDirectory)
    {
        BaseDirectory = baseDirectory;
        return this;
    }

    /// <summary>
    /// Adds a single file to the artifacts definition.
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public ArtifactsSection AddFile(string file)
    {
        Files.Add(file);
        return this;
    }

    /// <summary>
    /// Adds a range of files to the artifacts definition.
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    public ArtifactsSection AddFiles(IEnumerable<string> files)
    {
        Files.AddRange(files);
        return this;
    }

    /// <summary>
    /// Converts the artifacts section to the dictionary part of the BuildSpec.
    /// </summary>
    /// <returns></returns>
    public IDictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> dict = new()
        {
            { "baseDirectory", BaseDirectory },
            { "files", Files.ToArray() }
        };

        return dict;
    }
}