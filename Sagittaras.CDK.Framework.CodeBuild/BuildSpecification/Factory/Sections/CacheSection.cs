using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;

namespace Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Factory.Sections;

/// <summary>
/// Base implementation of the <see cref="ICacheSection"/>.
/// </summary>
public class CacheSection : ICacheSection
{
    /// <summary>
    /// Paths, files or patterns to cache.
    /// </summary>
    private readonly List<string> _paths = new();

    /// <inheritdoc />
    public string SectionName => "cache";

    /// <inheritdoc />
    public IDictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>
        {
            { "paths", _paths.ToArray() }
        };
    }

    /// <inheritdoc />
    public ICacheSection AddPath(string path)
    {
        _paths.Add(path);
        return this;
    }

    /// <inheritdoc />
    public ICacheSection AddPaths(IEnumerable<string> paths)
    {
        _paths.AddRange(paths);
        return this;
    }
}