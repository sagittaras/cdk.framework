using Amazon.CDK.AWS.CodePipeline;
using Constructs;
using Sagittaras.CDK.Framework.Factory;

namespace Sagittaras.CDK.Framework.CodePipeline.Stages;

public abstract class ActionBuilder<TAction> : CdkFactory<TAction>, IActionBuilder
    where TAction : IAction
{
    protected ActionBuilder(Construct scope, string name) : base(scope, name)
    {
        ActionName = name;
    }

    /// <inheritdoc />
    public string ActionName { get; }

    /// <inheritdoc />
    public abstract override TAction Construct();

    /// <inheritdoc />
    IAction ICdkFactory<IAction>.Construct()
    {
        return Construct();
    }
}