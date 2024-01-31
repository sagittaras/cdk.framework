using Constructs;

namespace Sagittaras.CDK.Framework.Factory;

/// <summary>
/// Describes an interface of the class that helps to build the constructs in more readable way.
/// </summary>
/// <typeparam name="TConstruct">Type of the construct the factory is creating.</typeparam>
/// <typeparam name="TProps">Type of props used for building the construct.</typeparam>
public interface IConstructFactory<out TConstruct, out TProps> : IConstruct, ICdkFactory<TConstruct>
    where TConstruct : IConstruct
    where TProps : class
{
    /// <summary>
    /// Current props instance used for building the construct.
    /// </summary>
    TProps Props { get; }
}