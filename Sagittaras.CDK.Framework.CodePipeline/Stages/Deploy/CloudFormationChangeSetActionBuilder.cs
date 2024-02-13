using Amazon.CDK.AWS.CodePipeline.Actions;

namespace Sagittaras.CDK.Framework.CodePipeline.Stages.Deploy;

/// <summary>
///     Creates a action that is responsible for creating a ChangeSet for a CloudFormation stack.
/// </summary>
public class CloudFormationChangeSetActionBuilder : ActionBuilder<CloudFormationCreateReplaceChangeSetAction>
{
    private readonly CloudFormationCreateReplaceChangeSetActionProps _props;
    private readonly Dictionary<string, object> _parameterOverrides = new();

    public CloudFormationChangeSetActionBuilder(PipelineStageBuilder scope, string name) : base(scope, name)
    {
        _props = new CloudFormationCreateReplaceChangeSetActionProps
        {
            ActionName = name
        };
    }

    /// <summary>
    ///     Configures the name of the change set.
    /// </summary>
    /// <param name="changeSetName"></param>
    /// <returns></returns>
    public CloudFormationChangeSetActionBuilder AsChangeSet(string changeSetName)
    {
        _props.ChangeSetName = changeSetName;
        return this;
    }

    /// <summary>
    ///     Sets the name of the stack to apply the change set to.
    /// </summary>
    /// <param name="stackName"></param>
    /// <returns></returns>
    public CloudFormationChangeSetActionBuilder ForStack(string stackName)
    {
        _props.StackName = stackName;
        return this;
    }

    /// <summary>
    ///     Enables the admin permissions for the change set.
    /// </summary>
    /// <returns></returns>
    public CloudFormationChangeSetActionBuilder WithAdminPermission()
    {
        _props.AdminPermissions = true;
        return this;
    }

    /// <summary>
    ///     Sets the input artifact name and the file which is used from the artifact for the template.
    /// </summary>
    /// <param name="inputArtifact"></param>
    /// <param name="templateFile"></param>
    /// <returns></returns>
    public CloudFormationChangeSetActionBuilder FromTemplateFile(string inputArtifact, string templateFile)
    {
        _props.TemplatePath = UseArtifact(inputArtifact).AtPath(templateFile);
        return this;
    }

    /// <summary>
    ///     Sets template artifact and the file which is used from the artifact for the template configuration.
    /// </summary>
    /// <param name="templateArtifact"></param>
    /// <param name="configurationFile"></param>
    /// <returns></returns>
    public CloudFormationChangeSetActionBuilder UseTemplateConfiguration(string templateArtifact, string configurationFile)
    {
        _props.TemplateConfiguration = UseArtifact(templateArtifact).AtPath(configurationFile);
        return this;
    }

    /// <summary>
    ///     Sets a parameter override for the change set.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public CloudFormationChangeSetActionBuilder WithParameterOverride(string key, object value)
    {
        _parameterOverrides[key] = value;
        return this;
    }

    /// <inheritdoc />
    public override CloudFormationCreateReplaceChangeSetAction Construct()
    {
        _props.ParameterOverrides = _parameterOverrides;
        return new CloudFormationCreateReplaceChangeSetAction(_props);
    }
}