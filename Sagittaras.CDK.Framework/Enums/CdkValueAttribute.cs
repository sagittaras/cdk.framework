namespace Sagittaras.CDK.Framework.Enums;

/// <summary>
/// Marks enum value with a valid translation to CDK value.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class CdkValueAttribute : Attribute
{
    /// <summary>
    /// Translated value for CDK.
    /// </summary>
    public string Value { get; }

    public CdkValueAttribute(string value)
    {
        Value = value;
    }
}