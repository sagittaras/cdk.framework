using Constructs;
using Sagittaras.CDK.Framework.Extensions;

namespace Sagittaras.CDK.Framework.Factory;

/// <summary>
/// Abstract implementation of base CDK factory.
/// </summary>
/// <typeparam name="TOutput">CDK type created by the factory.</typeparam>
public abstract class CdkFactory<TOutput> : Construct, ICdkFactory<TOutput>
{
    protected CdkFactory(Construct scope, string name) : base(scope, name.ToResourceId())
    {
    }

    /// <inheritdoc />
    public abstract TOutput Construct();
}