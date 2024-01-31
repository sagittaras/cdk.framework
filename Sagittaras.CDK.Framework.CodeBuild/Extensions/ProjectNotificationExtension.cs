using Amazon.CDK.AWS.Chatbot;
using Amazon.CDK.AWS.CodeBuild;
using Amazon.CDK.AWS.CodeStarNotifications;
using Sagittaras.CDK.Framework.Extensions;

namespace Sagittaras.CDK.Framework.CodeBuild.Extensions;

/// <summary>
/// Allows easy configuration of the CodeBuild project notification.
/// </summary>
public static class ProjectNotificationExtension
{
    /// <summary>
    /// Construct predefined set of rules for CodeBuild project which is part of CodePipeline.
    /// </summary>
    /// <remarks>
    /// Only project failures are reported in pipeline.
    /// </remarks>
    /// <param name="project"></param>
    /// <param name="slackChannel"></param>
    public static void AddPipelineNotification(this IProject project, ISlackChannelConfiguration slackChannel)
    {
        project.NotifyOnBuildFailed($"{project.ProjectName.ToResourceId()}_on-failure", slackChannel, new NotificationRuleOptions
        {
            Enabled = true,
            DetailType = DetailType.FULL,
            NotificationRuleName = $"{project.ProjectName.ToResourceId()}_on-failure"
        });
    }

    /// <summary>
    /// Adds notification of all build phases.
    /// </summary>
    /// <remarks>
    /// Suitable when the project is standalone. This is reporting every build event.
    /// </remarks>
    /// <param name="project"></param>
    /// <param name="slackChannel"></param>
    public static void AddNotifications(this IProject project, ISlackChannelConfiguration slackChannel)
    {
        project.NotifyOn($"{project.ProjectName.ToResourceId()}_notifications", slackChannel, new ProjectNotifyOnOptions
        {
            Enabled = true,
            DetailType = DetailType.FULL,
            NotificationRuleName = $"{project.ProjectName.ToResourceId()}_notifications",
            Events = new[]
            {
                ProjectNotificationEvents.BUILD_IN_PROGRESS,
                ProjectNotificationEvents.BUILD_FAILED,
                ProjectNotificationEvents.BUILD_STOPPED,
                ProjectNotificationEvents.BUILD_SUCCEEDED
            }
        });
    }
}