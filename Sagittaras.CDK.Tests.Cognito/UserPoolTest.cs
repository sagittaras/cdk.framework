using Amazon.CDK.Assertions;
using Sagittaras.CDK.Framework.Cognito.UserPools;
using Sagittaras.CDK.Testing.Cognito.UserPool;
using Xunit;

namespace Sagittaras.CDK.Tests.Cognito;

public class UserPoolTest : ConstructTest
{
    private const string PoolName = "TestPool";

    /// <summary>
    ///     Tests most basic creation of the user pool.
    /// </summary>
    [Fact]
    public void Test_BaseConstruct()
    {
        new UserPoolFactory(Stack, PoolName)
            .Construct();

        Template template = StackTemplate;

        new UserPoolAssertion()
            .WithUserPoolName(PoolName)
            .Assert(template);
    }

    /// <summary>
    ///     Tests setting of automatically verified attributes.
    /// </summary>
    [Fact]
    public void Test_AutoVerifiedAttribute()
    {
        const string singleVerify = "SingleVerify";
        const string multipleVerify = "MultipleVerify";

        new UserPoolFactory(Stack, singleVerify)
            .AutomaticallyVerify(x => x.Email)
            .Construct();

        new UserPoolFactory(Stack, multipleVerify)
            .AutomaticallyVerify(x => x.Email)
            .AutomaticallyVerify(x => x.Phone)
            .Construct();

        Template template = StackTemplate;

        new UserPoolAssertion()
            .WithUserPoolName(singleVerify)
            .WithAutoVerifiedAttribute(AutoVerifiedAttributes.Email)
            .Assert(template);

        new UserPoolAssertion()
            .WithUserPoolName(multipleVerify)
            .WithAutoVerifiedAttribute(AutoVerifiedAttributes.Email)
            .WithAutoVerifiedAttribute(AutoVerifiedAttributes.PhoneNumber)
            .Assert(template);
    }
}