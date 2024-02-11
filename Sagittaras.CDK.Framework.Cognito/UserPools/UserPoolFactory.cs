using System.Linq.Expressions;
using Amazon.CDK.AWS.Cognito;
using Constructs;
using Sagittaras.CDK.Framework.Factory;
using Sagittaras.CDK.Framework.Props;

namespace Sagittaras.CDK.Framework.Cognito.UserPools;

/// <summary>
///     Factory for constructing Amazon Cognito User Pool.
/// </summary>
public partial class UserPoolFactory : ConstructFactory<UserPool, UserPoolProps>
{
    public UserPoolFactory(Construct scope, string userPoolName) : base(scope, userPoolName)
    {
        Props = new UserPoolProps
        {
            UserPoolName = Cloudspace.ResourceName(userPoolName)
        };
    }

    /// <inheritdoc />
    public override UserPoolProps Props { get; }

    /// <inheritdoc />
    public override UserPool Construct()
    {
        UserPool pool = new(this, "pool", Props);
        AssignDomainsToPool(pool);

        return pool;
    }

    /// <summary>
    ///     Sets the method of Account recovery.
    /// </summary>
    /// <param name="recovery"></param>
    /// <returns></returns>
    public UserPoolFactory UsesAccountRecovery(AccountRecovery recovery)
    {
        Props.AccountRecovery = recovery;
        return this;
    }

    /// <summary>
    ///     Creates a configuration for usage of Simple Email Service for emails from user pool.
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    public UserPoolFactory ConfigureSimpleEmailService(Action<UserPoolSESOptions> configure)
    {
        UserPoolSESOptions options = new();
        configure.Invoke(options);

        Props.Email = UserPoolEmail.WithSES(options);
        return this;
    }

    /// <summary>
    ///     Configures which property is automatically verified.
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public UserPoolFactory AutomaticallyVerify<TProperty>(Expression<Func<AutoVerifiedAttrs, TProperty>> expression)
    {
        Props.AutoVerify ??= new AutoVerifiedAttrs();
        ExpressionMapper.SetValue(expression, Props.AutoVerify, true);

        return this;
    }

    /// <summary>
    ///     Configures allowed aliases.
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public UserPoolFactory AllowsAlias<TProperty>(Expression<Func<SignInAliases, TProperty>> expression)
    {
        Props.SignInAliases ??= new SignInAliases();
        ExpressionMapper.SetValue(expression, Props.SignInAliases, true);

        return this;
    }

    /// <summary>
    ///     Configures the sign-in to be case insensitive.
    /// </summary>
    /// <returns></returns>
    public UserPoolFactory IsCaseInsensitive()
    {
        Props.SignInCaseSensitive = false;
        return this;
    }

    /// <summary>
    ///     Configures usage of the standard attribute.
    /// </summary>
    /// <param name="expression">Expression returning the attribute to be configured.</param>
    /// <param name="required"></param>
    /// <param name="mutable"></param>
    /// <typeparam name="TProperty"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public UserPoolFactory UseStandardAttribute<TProperty>(Expression<Func<StandardAttributes, TProperty>> expression, bool required = false, bool mutable = true)
    {
        Props.StandardAttributes ??= new StandardAttributes();
        ExpressionMapper.SetValue(expression, Props.StandardAttributes, new StandardAttribute
        {
            Required = required,
            Mutable = mutable
        });

        return this;
    }
}