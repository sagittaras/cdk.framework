namespace Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;

/// <summary>
/// Factory describing a artifacts section of the BuildSpec.
/// </summary>
public interface IArtifactsSection : IBuildSpecSection
{
    /// <summary>
    /// Configures the base directory from which the artifacts are to be uploaded.
    /// </summary>
    /// <param name="baseDirectory"></param>
    /// <returns></returns>
    IArtifactsSection WithBaseDirectory(string baseDirectory);

    /// <summary>
    /// Adds a file to the artifacts definition.
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    IArtifactsSection AddFile(string file);

    /// <summary>
    /// Adds a range of files to the artifacts definition.
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    IArtifactsSection AddFiles(IEnumerable<string> files);
}