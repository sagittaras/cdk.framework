using Amazon.CDK.AWS.CodePipeline;
using Sagittaras.CDK.Framework.Factory;

namespace Sagittaras.CDK.Framework.CodePipeline.Stages.Abstractions;

/// <summary>
/// Describes a builder class for a single pipeline stage.
/// </summary>
public interface IPipelineStageBuilder : ICdkFactory<IStageProps>
{
    /// <summary>
    /// Name of the pipeline stage.
    /// </summary>
    string StageName { get; }

    /// <summary>
    /// Adds existing action to the stage.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    IPipelineStageBuilder AddAction(IAction action);

    /// <summary>
    ///     Gets factory for the action with the given name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IAction GetAction(string name);

    /// <summary>
    /// Uses an artifact with the given name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Artifact_ UseArtifact(string name);
}