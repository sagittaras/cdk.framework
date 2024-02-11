using Sagittaras.CDK.Framework.Enums;

namespace Sagittaras.CDK.Testing.Cognito.UserPool;

[Flags]
public enum AliasAttributes
{
    [CdkValue("email")]
    Email,

    [CdkValue("phone_number")]
    PhoneNumber,

    [CdkValue("preferred_username")]
    PreferredUsername
}