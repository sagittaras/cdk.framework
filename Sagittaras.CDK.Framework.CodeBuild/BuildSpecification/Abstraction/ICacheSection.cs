namespace Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;

/// <summary>
/// Defines a cache section of the buildspec, configuring which paths are to be cached.
/// </summary>
public interface ICacheSection : IBuildSpecSection
{
    /// <summary>
    /// Adds a path to the cache section.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    ICacheSection AddPath(string path);

    /// <summary>
    /// Adds a range of paths to the cache section.
    /// </summary>
    /// <param name="paths"></param>
    /// <returns></returns>
    ICacheSection AddPaths(IEnumerable<string> paths);
}