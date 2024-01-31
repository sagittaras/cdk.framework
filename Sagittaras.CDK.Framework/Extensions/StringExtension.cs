namespace Sagittaras.CDK.Framework.Extensions;

public static class StringExtension
{
    /// <summary>
    /// Converts the string to safe value for AWS resource ID.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToResourceId(this string str)
    {
        return str
            .ToLower()
            .Replace(".", "")
            .Replace(" ", "-")
            ;
    }
}