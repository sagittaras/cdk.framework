using Amazon.CDK.AWS.CodePipeline;
using Amazon.CDK.AWS.CodeStarNotifications;
using Constructs;
using Sagittaras.CDK.Framework.Extensions;

namespace Sagittaras.CDK.Framework.CodePipeline.Extensions;

public static class PipelineExtension
{
    /// <summary>
    /// Configures a notifications for the slack channel to the pipeline.
    /// </summary>
    /// <param name="pipeline"></param>
    /// <param name="scope"></param>
    /// <param name="target">Target for notification.</param>
    public static void AddSlackNotification(this IPipeline pipeline, Construct scope, INotificationRuleTarget target)
    {
        string resourceId = pipeline.PipelineName.ToResourceId();
        pipeline.NotifyOn($"{resourceId}-notification", target, new PipelineNotifyOnOptions
        {
            Events = new[]
            {
                PipelineNotificationEvents.PIPELINE_EXECUTION_CANCELED,
                PipelineNotificationEvents.PIPELINE_EXECUTION_FAILED,
                PipelineNotificationEvents.PIPELINE_EXECUTION_SUCCEEDED,
                PipelineNotificationEvents.PIPELINE_EXECUTION_STARTED
            }
        });
    }
}