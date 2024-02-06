using Sagittaras.CDK.Framework.Enums;

namespace Sagittaras.CDK.Testing.Route53.KSK;

public enum KskStatus
{
    [CdkValue("ACTIVE")]
    Active,

    [CdkValue("INACTIVE")]
    Inactive
}