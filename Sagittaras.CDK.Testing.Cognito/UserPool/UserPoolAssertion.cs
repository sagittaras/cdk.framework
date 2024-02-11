using Sagittaras.CDK.Framework;
using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Cognito.UserPool;

/// <summary>
///     Assertion for AWS::Cognito::UserPool.
/// </summary>
public class UserPoolAssertion : ResourceAssertion<UserPoolProperties>
{
    /// <inheritdoc />
    public override string Type => "AWS::Cognito::UserPool";

    /// <summary>
    ///     Sets the expected user pool name.
    /// </summary>
    /// <param name="userPoolName"></param>
    /// <returns></returns>
    public UserPoolAssertion WithUserPoolName(string userPoolName)
    {
        SetProperty(x => x.UserPoolName = Cloudspace.ResourceName(userPoolName));
        return this;
    }

    /// <summary>
    ///     Sets the expected flag of auto verified attribute.
    /// </summary>
    /// <param name="attribute"></param>
    /// <returns></returns>
    public UserPoolAssertion WithAutoVerifiedAttribute(AutoVerifiedAttributes attribute)
    {
        SetProperty(x => x.AutoVerifiedAttributes |= attribute);
        return this;
    }

    /// <summary>
    ///     Sets expected flag of alias attribute.
    /// </summary>
    /// <param name="attribute"></param>
    /// <returns></returns>
    public UserPoolAssertion AllowsAlias(AliasAttributes attribute)
    {
        SetProperty(x => x.AliasAttributes |= attribute);
        return this;
    }
}