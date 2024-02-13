using Amazon.CDK.AWS.CodePipeline;
using Sagittaras.CDK.Framework.Factory;

namespace Sagittaras.CDK.Framework.CodePipeline.Stages;

/// <summary>
///     Extends basic CDK factory with the ability to create action for CodePipeline.
/// </summary>
public interface IActionBuilder : ICdkFactory<IAction>
{
    /// <summary>
    ///     Name of the action the factory is building.
    /// </summary>
    string ActionName { get; }
}