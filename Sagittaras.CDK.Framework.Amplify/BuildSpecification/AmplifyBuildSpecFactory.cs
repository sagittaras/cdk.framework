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
        RegisterSection<IEnvironmentSection, EnvironmentSection>();
        RegisterSection<IFrontendSection, FrontendSection>();
    }

    /// <inheritdoc />
    public IEnvironmentSection Environment => GetRequiredSection<IEnvironmentSection>();

    /// <inheritdoc />
    public IFrontendSection Frontend => GetRequiredSection<IFrontendSection>();
}