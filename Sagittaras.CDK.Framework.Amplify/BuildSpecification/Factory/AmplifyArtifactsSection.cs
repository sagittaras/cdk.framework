using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Factory.Sections;

namespace Sagittaras.CDK.Framework.Amplify.BuildSpecification.Factory;

public class AmplifyArtifactsSection : ArtifactsSection
{
    /// <summary>
    /// Override the base directory key.
    /// </summary>
    protected override string BaseDirectoryKey => "baseDirectory";
}