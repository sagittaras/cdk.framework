using Amazon.CDK.AWS.CodeBuild;
using Amazon.CDK.AWS.SecretsManager;
using Sagittaras.CDK.Framework.IAM.Policies;

namespace Sagittaras.CDK.Framework.CodeBuild.Extensions;

/// <summary>
/// Extension classes helping easily assign policies to the CodeBuild projects.
/// </summary>
public static class ProjectPolicyExtension
{
    /// <summary>
    /// Adds policies to the CodeBuild project that are required by the CDK.
    /// </summary>
    /// <param name="project"></param>
    public static void AddPoliciesForCdk(this IProject project)
    {
        project.AddToRolePolicy(CdkPolicy.DescribeAvailabilityZones);
        project.AddToRolePolicy(CdkPolicy.AssumeRole);
    }

    /// <summary>
    /// Adds a policy that allows the CodeBuild project to authorize against ECR and push images.
    /// </summary>
    /// <param name="project"></param>
    public static void AddEcrPushImage(this IProject project)
    {
        project.AddToRolePolicy(Ec2Policy.EcrAuthorization);
        project.AddToRolePolicy(Ec2Policy.EcrPushImage);
    }

    /// <summary>
    /// Adds policy that allows authorization against CodeArtifact.
    /// </summary>
    /// <param name="project"></param>
    public static void AddCodeArtifactAuthorization(this IProject project)
    {
        project.AddToRolePolicy(DeveloperPolicy.CodeArtifactAuthorization);
    }

    /// <summary>
    /// Adds policy that allows authorization to pull packages.
    /// </summary>
    /// <param name="project"></param>
    public static void AddCodeArtifactReadOnly(this IProject project)
    {
        project.AddToRolePolicy(DeveloperPolicy.CodeArtifactReadOnly);
    }

    /// <summary>
    /// Adds policy that allows  to push packages.
    /// </summary>
    /// <param name="project"></param>
    public static void AddCodeArtifactPublish(this IProject project)
    {
        project.AddToRolePolicy(DeveloperPolicy.CodeArtifactPublish);
    }

    /// <summary>
    /// Allows the project to receive information about the Lambda layers.
    /// </summary>
    /// <param name="project"></param>
    public static void AddLambdaLayerAccess(this IProject project)
    {
        project.AddToRolePolicy(LambdaPolicy.GetLayerVersion);
    }

    /// <summary>
    /// Allows the project to read passed secrets.
    /// </summary>
    /// <param name="project"></param>
    /// <param name="secrets"></param>
    public static void AddSecretsAccess(this IProject project, params ISecret[] secrets)
    {
        project.AddToRolePolicy(SecretsPolicy.AllowReadSecrets(secrets));
    }
}