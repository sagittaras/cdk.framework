using Constructs;
using Sagittaras.CDK.Framework.Factory;

namespace Sagittaras.CDK.Framework.CodeDeploy.Applications;

/// <summary>
///     Common factory construct for defining the CodeDeploy applications.
/// </summary>
/// <typeparam name="TApplication">Type of resulting application.</typeparam>
/// <typeparam name="TProps">Properties for the application.</typeparam>
public abstract class CodeDeployFactory<TApplication, TProps> : ConstructFactory<TApplication, TProps>
    where TApplication : IConstruct
    where TProps : class
{
    protected CodeDeployFactory(Construct scope, string applicationName) : base(scope, applicationName)
    {
        ApplicationName = Cloudspace.ResourceName(applicationName);
    }

    /// <summary>
    ///     Name of the application prefixed with the Cloud space name.
    /// </summary>
    protected string ApplicationName { get; }
}