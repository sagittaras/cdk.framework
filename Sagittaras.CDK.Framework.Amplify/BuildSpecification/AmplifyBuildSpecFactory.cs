using Sagittaras.CDK.Framework.Amplify.BuildSpecification.Abstraction;
using Sagittaras.CDK.Framework.Amplify.BuildSpecification.Factory;
using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;
using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Factory;
using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Factory.Sections;

namespace Sagittaras.CDK.Framework.Amplify.BuildSpecification;

/// <summary>
/// Build Spec factory for the amplify applications.
/// </summary>
public class AmplifyBuildSpecFactory : BuildSpecFactory, IAmplifyBuildSpecFactory
{
    public AmplifyBuildSpecFactory()
    {
        Version(1);

        RegisterSection<IEnvironmentSection, EnvironmentSection>();
        RegisterSection<IFrontendSection, FrontendSection>();
        RegisterSection<ITestSection, TestSection>();
    }

    /// <inheritdoc />
    public IEnvironmentSection Environment => GetRequiredSection<IEnvironmentSection>();

    /// <inheritdoc />
    public IFrontendSection Frontend => GetRequiredSection<IFrontendSection>();

    /// <inheritdoc />
    public ITestSection Test => GetRequiredSection<ITestSection>();

    /// <summary>
    /// Configures the selected frontend phase.
    /// </summary>
    /// <param name="phase"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public AmplifyBuildSpecFactory FrontendPhase(BuildPhase phase, Action<IBuildPhaseSection> configure)
    {
        configure.Invoke(Frontend.Phases.Phase(phase));
        return this;
    }

    /// <summary>
    /// Configures the selected test phase.
    /// </summary>
    /// <param name="phase"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public AmplifyBuildSpecFactory TestPhase(TestPhase phase, Action<ITestPhaseSection> configure)
    {
        configure.Invoke(Test.Phase(phase));
        return this;
    }
}