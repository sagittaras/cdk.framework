using Sagittaras.CDK.Framework.Amplify.Buildspecs.Sections;

namespace Sagittaras.CDK.Framework.Amplify.Buildspecs;

/// <summary>
/// BuildSpec factory for Next.js applications with the support of server-side rendering.
/// </summary>
public class ServerSideBuildspecFactory : AmplifyBuildspecFactory
{
    public ServerSideBuildspecFactory()
    {
        FrontendPhase(PhaseType.PreBuild)
            .Command("npm ci")
            ;

        FrontendPhase(PhaseType.Build)
            .Command("env | grep -e REACT_APP_API_BASE_URL -e REACT_APP_API_HOST -e REACT_APP_API_PORT -e REACT_APP_API_SCHEME >> .env.production")
            .Command("npm run build")
            ;

        FrontendArtifacts()
            .WithBaseDirectory(".next")
            .AddFile("**/*")
            ;

        FrontendCache()
            .AddPath("node_modules/**/*")
            ;
    }
}