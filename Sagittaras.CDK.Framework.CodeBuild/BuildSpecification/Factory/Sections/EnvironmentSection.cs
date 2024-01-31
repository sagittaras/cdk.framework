using Amazon.CDK.AWS.SecretsManager;
using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;

namespace Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Factory.Sections;

/// <summary>
/// Describes an environment for the CodeBuild project. <see cref="IEnvironmentSection"/> implementation.
/// </summary>
public class EnvironmentSection : IEnvironmentSection
{
    /// <summary>
    /// Environment variables.
    /// </summary>
    private readonly Dictionary<string, string> _variables = new();

    /// <summary>
    /// Environment variables that are exported from the project.
    /// </summary>
    private readonly List<string> _exportedVariables = new();

    /// <summary>
    /// Values stored in secrets manager.
    /// </summary>
    private readonly Dictionary<string, string> _secrets = new();

    /// <summary>
    /// List of secrets that were used in the section.
    /// </summary>
    private readonly List<ISecret> _usedSecrets = new();

    /// <inheritdoc />
    public string SectionName => "env";

    /// <inheritdoc />
    public IReadOnlyCollection<ISecret> Secrets => _usedSecrets.AsReadOnly();

    /// <inheritdoc />
    public IDictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> dict = new();
        if (_variables.Count > 0)
        {
            dict.Add("variables", _variables);
        }

        if (_exportedVariables.Count > 0)
        {
            dict.Add("exported-variables", _exportedVariables.ToArray());
        }

        if (_secrets.Count > 0)
        {
            dict.Add("secrets-manager", _secrets);
        }

        return dict;
    }

    /// <inheritdoc />
    public IEnvironmentSection AddVariable(string key, string value)
    {
        _variables[key] = value;
        return this;
    }

    /// <inheritdoc />
    public IEnvironmentSection AddExportedVariable(string key)
    {
        _exportedVariables.Add(key);
        return this;
    }

    /// <inheritdoc />
    public IEnvironmentSection AddSecretValue(string key, ISecret secret)
    {
        _usedSecrets.Add(secret);
        _secrets[key] = secret.SecretArn;
        return this;
    }

    /// <inheritdoc />
    public IEnvironmentSection AddSecretValue(string key, ISecret secret, string jsonField)
    {
        _usedSecrets.Add(secret);
        _secrets[key] = $"{secret.SecretArn}:{jsonField}";
        return this;
    }
}