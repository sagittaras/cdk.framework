using System.Reflection;
using Amazon.CDK.AWS.Lambda;

namespace Sagittaras.CDK.Framework.Props;

/// <summary>
///     Simple class used to map common properties of resources props.
/// </summary>
/// <remarks>
///     This class is used to solve bad OOP design of AWS CDK classes. For example:
///     All props for different Lambda functions are using the same interface <see cref="IFunctionOptions" /> to define common lambda props,
///     but interfaces in CDK doesn't support setters.
///     Although implementation of this interface in form of <see cref="FunctionOptions" /> does exists, the lambda function props only implements the interface.
///     This is created as the easiest solution how to transform properties from custom common class (or logical common class in CDK) to actual props
///     for created resource.
/// </remarks>
public static class PropsMapper
{
    /// <summary>
    ///     Remaps the property values from the source class to target props class.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="props"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TProps"></typeparam>
    public static void Map<TSource, TProps>(TSource source, TProps props)
    {
        Type optionsType = typeof(TSource);
        Type propsType = typeof(TProps);

        Dictionary<string, object> definedOptions = optionsType.GetProperties()
            .Select(x => new KeyValuePair<string, object?>(x.Name, x.GetValue(source)))
            .Where(x => x.Value is not null)
            .ToDictionary(x => x.Key, x => x.Value!);

        Dictionary<string, PropertyInfo> targetProps = propsType.GetProperties()
            .Where(x => definedOptions.ContainsKey(x.Name))
            .ToDictionary(x => x.Name, x => x);

        foreach ((string name, object value) in definedOptions) targetProps[name].SetValue(props, value);
    }
}