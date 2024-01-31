using Amazon.CDK.AWS.IAM;

namespace Sagittaras.CDK.Framework.IAM.Extensions;

/// <summary>
/// Extension methods providing easy and more readable access to predefined & common policies.
/// </summary>
public static class GroupFactoryPolicyExtension
{
    /// <summary>
    /// Adds a read access to basic cloud watch analytics & logs services.
    /// </summary>
    /// <param name="factory"></param>
    /// <returns></returns>
    public static GroupFactory WithCloudWatchReadAccess(this GroupFactory factory)
    {
        return factory.WithManagedPolicyFromArn("cloudwatch-readonly", "arn:aws:iam::aws:policy/CloudWatchReadOnlyAccess")
                .WithManagedPolicyFromArn("xray-readonly", "arn:aws:iam::aws:policy/AWSXrayReadOnlyAccess")
            ;
    }

    /// <summary>
    /// Adds a read access to basic repository services like CodeArtifact or ECR.
    /// </summary>
    /// <param name="factory"></param>
    /// <returns></returns>
    public static GroupFactory WithRepositoriesReadAccess(this GroupFactory factory)
    {
        return factory.WithManagedPolicyFromArn("codeartifact-readonly", "arn:aws:iam::aws:policy/AWSCodeArtifactReadOnlyAccess")
            .WithManagedPolicyFromArn("ecr-readonly", "arn:aws:iam::aws:policy/AmazonEC2ContainerRegistryReadOnly");
        ;
    }

    /// <summary>
    /// Adds access to the deployment services like CodePipeline or CodeBuild.
    /// </summary>
    /// <param name="factory"></param>
    /// <returns></returns>
    public static GroupFactory WithDeploymentAccess(this GroupFactory factory)
    {
        return factory.WithPolicyStatement(new PolicyStatementProps
        {
            Sid = "CodePipelineAccess",
            Effect = Effect.ALLOW,
            Actions = new[]
                {
                    "codepipeline:List*",
                    "codepipeline:*PipelineExecution",
                    "codepipeline:Put*",
                    "codepipeline:Get*"
                },
            Resources = new[] { $"arn:aws:codepipeline:*:{Cloudspace.AccountId}:*" }
        }).WithPolicyStatement(new PolicyStatementProps
        {
            Sid = "CodeBuildAccess",
            Effect = Effect.ALLOW,
            Actions = new[]
                {
                    "codebuild:List*",
                    "codebuild:Get*",
                    "codebuild:Describe*",
                    "codebuild:*Build",
                    "codebuild:Batch*"
                },
            Resources = new[] { $"arn:aws:codebuild:*:{Cloudspace.AccountId}:project/*" }
        })
            ;
    }

    /// <summary>
    /// Adds basic S3 access to the users. 
    /// </summary>
    /// <param name="factory"></param>
    /// <returns></returns>
    public static GroupFactory WithS3Access(this GroupFactory factory)
    {
        return factory.WithPolicyStatement(new PolicyStatementProps
        {
            Sid = "S3Access",
            Effect = Effect.ALLOW,
            Actions = new[]
            {
                "s3:Get*",
                "S3:Describe*",
                "s3:Put*",
                "s3:Upload*",
                "s3:Replicate*",
                "s3:Restore*"
            },
            Resources = new[]
            {
                "arn:aws:s3:::*"
            }
        });
    }
}