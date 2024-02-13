using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.CodePipeline.Pipeline.ArtifactStore;

public class ArtifactStoreProperties : ResourceProperties
{
    public string? Location { get; set; }
    public ArtifactStoreType? Type { get; set; }
}