using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Cognito.UserPool;

/// <summary>
///     Properties for AWS::Cognito::UserPool.
/// </summary>
public class UserPoolProperties : ResourceProperties
{
    public string? UserPoolName { get; set; }
    public AutoVerifiedAttributes? AutoVerifiedAttributes { get; set; }
    public AliasAttributes? AliasAttributes { get; set; }
}