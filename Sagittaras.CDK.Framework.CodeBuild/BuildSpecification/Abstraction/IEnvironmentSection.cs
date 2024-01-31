using Amazon.CDK.AWS.SecretsManager;

namespace Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;

/// <summary>
/// Describes an environment section that helps configure further the variables.
/// </summary>
public interface IEnvironmentSection : IBuildSpecSection
{
    /// <summary>
    /// Collection of Secrets that were assigned to the section.
    /// </summary>
    /// <remarks>
    /// This collection is used to configure the project policies.
    /// </remarks>
    /// <see cref="AddSecretValue(string,Amazon.CDK.AWS.SecretsManager.ISecret)"/>
    /// <see cref="AddSecretValue(string,Amazon.CDK.AWS.SecretsManager.ISecret,string)"/>
    IReadOnlyCollection<ISecret> Secrets { get; }

    /// <summary>
    /// Adds new environment variable definition to the BuildSpec.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    IEnvironmentSection AddVariable(string key, string value);

    /// <summary>
    /// Adds a name of environment variable that will be exported from the project.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    IEnvironmentSection AddExportedVariable(string key);

    /// <summary>
    /// Adds a value from secrets manager as an environment variable.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="secret"></param>
    /// <returns></returns>
    IEnvironmentSection AddSecretValue(string key, ISecret secret);

    /// <summary>
    /// Adds a value from secrets manager as an environment variable.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="secret"></param>
    /// <param name="jsonField"></param>
    /// <returns></returns>
    IEnvironmentSection AddSecretValue(string key, ISecret secret, string jsonField);
}