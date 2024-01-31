using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;

namespace Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Factory.Sections;

/// <summary>
/// Describes the CodeBuild artifacts. <see cref="IArtifactsSection"/> implementation.
/// </summary>
public class ArtifactsSection : IArtifactsSection
{
    /// <summary>
    /// Base directory for the artifacts.
    /// </summary>
    private string _baseDirectory = ".";

    /// <summary>
    /// List of files or patterns to include in the artifacts.
    /// </summary>
    private readonly List<string> _files = new();

    /// <inheritdoc />
    public string SectionName => "artifacts";

    /// <inheritdoc />
    public IDictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>
        {
            { "base-directory", _baseDirectory },
            { "files", _files.ToArray() }
        };
    }

    /// <inheritdoc />
    public IArtifactsSection WithBaseDirectory(string baseDirectory)
    {
        _baseDirectory = baseDirectory;
        return this;
    }

    /// <inheritdoc />
    public IArtifactsSection AddFile(string file)
    {
        _files.Add(file);
        return this;
    }

    /// <inheritdoc />
    public IArtifactsSection AddFiles(IEnumerable<string> files)
    {
        _files.AddRange(files);
        return this;
    }
}