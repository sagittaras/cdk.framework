using Amazon.CDK.AWS.IAM;

namespace Sagittaras.CDK.Framework.IAM.Policies;

/// <summary>
/// References to the policies related to the EC2.
/// </summary>
public static class Ec2Policy
{
    /// <summary>
    /// Allows ECR authorization.
    /// </summary>
    public static PolicyStatement EcrAuthorization => new(new PolicyStatementProps
    {
        Sid = "ECRAuthorization",
        Effect = Effect.ALLOW,
        Actions = new[] { "ecr:GetAuthorizationToken" },
        Resources = new[] { "*" }
    });

    /// <summary>
    /// Allows pushing images to the ECR.
    /// </summary>
    public static PolicyStatement EcrPushImage => new(new PolicyStatementProps
    {
        Sid = "ECRPushImage",
        Effect = Effect.ALLOW,
        Actions = new[]
        {
            "ecr:BatchCheckLayerAvailability",
            "ecr:GetDownloadUrlForLayer",
            "ecr:GetRepositoryPolicy",
            "ecr:DescribeRepositories",
            "ecr:ListImages",
            "ecr:DescribeImages",
            "ecr:BatchGetImage",
            "ecr:GetLifecyclePolicy",
            "ecr:GetLifecyclePolicyPreview",
            "ecr:ListTagsForResource",
            "ecr:DescribeImageScanFindings",
            "ecr:InitiateLayerUpload",
            "ecr:UploadLayerPart",
            "ecr:CompleteLayerUpload",
            "ecr:PutImage"
        },
        Resources = new[] { $"arn:aws:ecr:*:{Cloudspace.AccountId}:repository/*" }
    });
}