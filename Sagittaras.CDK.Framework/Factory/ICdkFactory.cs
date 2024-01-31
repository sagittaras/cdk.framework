namespace Sagittaras.CDK.Framework.Factory;

/// <summary>
/// Base factory interface for creating CDK sources.
/// </summary>
/// <typeparam name="TOutput"></typeparam>
public interface ICdkFactory<out TOutput>
{
    /// <summary>
    /// Constructs the outputting type.
    /// </summary>
    /// <returns></returns>
    TOutput Construct();
}