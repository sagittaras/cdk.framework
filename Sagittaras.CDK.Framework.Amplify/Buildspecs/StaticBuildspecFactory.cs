using Sagittaras.CDK.Framework.Amplify.Buildspecs.Sections;

namespace Sagittaras.CDK.Framework.Amplify.Buildspecs;

/// <summary>
/// Amplify buildspec factory for static applications.
/// </summary>
public class StaticBuildspecFactory : AmplifyBuildspecFactory
{
    public StaticBuildspecFactory()
    {
        FrontendPhase(PhaseType.PreBuild)
            .Command("npm ci")
            ;

        FrontendPhase(PhaseType.Build)
            .Command("npm run build")
            ;

        FrontendArtifacts()
            .WithBaseDirectory("build")
            .AddFile("**/*")
            ;

        FrontendCache()
            .AddPath("node_modules/**/*")
            ;
    }
}