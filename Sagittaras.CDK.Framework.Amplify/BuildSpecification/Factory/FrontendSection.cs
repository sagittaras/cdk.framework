using Sagittaras.CDK.Framework.Amplify.BuildSpecification.Abstraction;
using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;
using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Factory.Sections;

namespace Sagittaras.CDK.Framework.Amplify.BuildSpecification.Factory;

/// <summary>
/// Describes the frontend section of the Amplify build specification.
/// </summary>
public class FrontendSection : IFrontendSection
{
    private AmplifyArtifactsSection? _artifacts;
    private CacheSection? _cache;

    /// <inheritdoc />
    public string SectionName => "frontend";

    /// <inheritdoc />
    public IPhasesSection Phases { get; } = new AmplifyPhasesSection();

    /// <inheritdoc />
    public IArtifactsSection Artifacts => _artifacts ??= new AmplifyArtifactsSection();

    /// <inheritdoc />
    public ICacheSection Cache => _cache ??= new CacheSection();

    /// <inheritdoc />
    public IDictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> dict = new()
        {
            { "phases", Phases.ToDictionary() }
        };

        if (_artifacts is not null)
        {
            dict.Add(_artifacts.SectionName, _artifacts.ToDictionary());
        }

        if (_cache is not null)
        {
            dict.Add(_cache.SectionName, _cache.ToDictionary());
        }

        return dict;
    }
}