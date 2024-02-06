using Amazon.CDK.AWS.Lambda;

namespace Sagittaras.CDK.Framework.Lambda.Extensions;

/// <summary>
///     Useful extension methods for Lambda function.
/// </summary>
public static class FunctionExtension
{
    /// <summary>
    ///     Shortcut method to add an alias with description to the function.
    /// </summary>
    /// <param name="function"></param>
    /// <param name="aliasName"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public static Alias AddAlias(this Function function, string aliasName, string? description)
    {
        return function.AddAlias(aliasName, new AliasOptions
        {
            Description = description
        });
    }
}