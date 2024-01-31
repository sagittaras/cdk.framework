using Amazon.CDK.AWS.CodeBuild;
using Sagittaras.CDK.Framework.Amplify.Buildspecs.Sections;

namespace Sagittaras.CDK.Framework.Amplify.Buildspecs;

/// <summary>
/// Build specification for the amplify.
/// </summary>
public class AmplifyBuildspecFactory : IAmplifyBuildspecFactory
{
    /// <summary>
    /// Version of the amplify's build spec.
    /// </summary>
    private static int Version => 1;

    /// <summary>
    /// Definition of the frontend resources.
    /// </summary>
    private readonly FrontendSection _frontendSection = new();

    /// <summary>
    /// Gets the configuration of frontend phase.
    /// </summary>
    /// <param name="phaseType"></param>
    /// <returns></returns>
    public BuildPhase FrontendPhase(PhaseType phaseType)
    {
        return _frontendSection.Phase(phaseType);
    }

    /// <summary>
    /// Gets the configuration of frontend artifacts.
    /// </summary>
    /// <returns></returns>
    public ArtifactsSection FrontendArtifacts()
    {
        return _frontendSection.Artifacts();
    }

    /// <summary>
    /// Gets the configuration of frontend caching.
    /// </summary>
    /// <returns></returns>
    public CacheSection FrontendCache()
    {
        return _frontendSection.Cache();
    }

    /// <inheritdoc />
    public BuildSpec ToBuildSpec()
    {
        Dictionary<string, object> dict = new()
        {
            { "version", Version },
            { "frontend", _frontendSection.ToDictionary() }
        };

        return BuildSpec.FromObjectToYaml(dict);
    }
}