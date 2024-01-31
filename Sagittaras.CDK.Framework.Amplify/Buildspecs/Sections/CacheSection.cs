namespace Sagittaras.CDK.Framework.Amplify.Buildspecs.Sections;

/// <summary>
/// Cache section of the BuildSpec file.
/// </summary>
public class CacheSection
{
    private readonly List<string> _paths = new();

    /// <summary>
    /// Adds a single path to the cache section.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public CacheSection AddPath(string path)
    {
        _paths.Add(path);
        return this;
    }

    /// <summary>
    /// Adds a enumerable of paths to the cache section.
    /// </summary>
    /// <param name="paths"></param>
    /// <returns></returns>
    public CacheSection AddPaths(IEnumerable<string> paths)
    {
        _paths.AddRange(paths);
        return this;
    }

    /// <summary>
    /// Converts the cache section to dictionary.
    /// </summary>
    /// <returns></returns>
    public IDictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>
        {
            { "paths", _paths.ToArray() }
        };
    }
}