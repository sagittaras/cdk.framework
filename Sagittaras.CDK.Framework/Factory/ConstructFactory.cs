using Constructs;

namespace Sagittaras.CDK.Framework.Factory;

/// <summary>
/// Abstract implementation of the construct factory.
/// </summary>
/// <remarks>
/// Factory is a CDK construct which serves as a scope.
/// </remarks>
/// <typeparam name="TConstruct">Type of construct the factory is building.</typeparam>
/// <typeparam name="TProps">Type of properties class used for building the construct.</typeparam>
public abstract class ConstructFactory<TConstruct, TProps> : CdkFactory<TConstruct>, IConstructFactory<TConstruct, TProps>
    where TConstruct : IConstruct
    where TProps : class
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="scope"></param>
    /// <param name="name"></param>
    protected ConstructFactory(Construct scope, string name) : base(scope, name)
    {
    }

    /// <inheritdoc />
    public abstract TProps Props { get; }
}