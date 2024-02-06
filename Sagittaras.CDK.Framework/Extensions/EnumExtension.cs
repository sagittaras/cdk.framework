using System.Reflection;
using Sagittaras.CDK.Framework.Enums;

namespace Sagittaras.CDK.Framework.Extensions;

public static class EnumExtension
{
    /// <summary>
    /// Gets the translated CDK value from the Enum value.
    /// </summary>
    /// <remarks>
    /// If the enum field & <see cref="CdkValueAttribute"/> are available. If not, value translation to string is returned.
    /// </remarks>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetCdkValue(this Enum value)
    {
        Type type = value.GetType();
        string? name = Enum.GetName(type, value);
        if (name is null)
        {
            return value.ToString();
        }

        FieldInfo? field = type.GetField(name);
        if (field is null)
        {
            return value.ToString();
        }

        CdkValueAttribute? attribute = field.GetCustomAttribute<CdkValueAttribute>();
        return attribute?.Value ?? value.ToString();
    }
}