using Sagittaras.CDK.Framework.Enums;

namespace Sagittaras.CDK.Testing.Cognito.UserPool;

[Flags]
public enum AutoVerifiedAttributes
{
    [CdkValue("email")]
    Email,

    [CdkValue("phone_number")]
    PhoneNumber
}