using System.Text.Json;
using Amazon.CDK;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.SecretsManager;
using Constructs;
using Sagittaras.CDK.Framework.Factory;
using Sagittaras.CDK.Framework.IAM.Extensions;

namespace Sagittaras.CDK.Framework.IAM;

/// <summary>
/// Factory for creating instance of IAM user.
/// </summary>
public class UserFactory : ConstructFactory<User, UserProps>
{
    /// <summary>
    /// List of groups to which the user will be assigned.
    /// </summary>
    private readonly List<IGroup> _groups = new();

    /// <summary>
    /// Generated secret with password if the user has console access.
    /// </summary>
    private Secret? _password;

    /// <summary>
    /// Define is the user has programmatic access allowed.
    /// </summary>
    private bool _hasProgrammaticAccess;

    public UserFactory(Construct scope, string username) : base(scope, username)
    {
        Props = new UserProps
        {
            UserName = username
        };
    }

    /// <summary>
    /// Defined props for creating the user.
    /// </summary>
    public override UserProps Props { get; }

    /// <inheritdoc />
    public override User Construct()
    {
        Props.Groups = _groups.ToArray();

        User user = new(this, "user", Props);
        user.AllowSecretsListing();

        _password?.GrantRead(user);
        if (_hasProgrammaticAccess)
        {
            ConstructSecretKeyForProgrammaticAccess(user);
        }

        return user;
    }

    /// <summary>
    /// Group to which the user will be assigned.
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    public UserFactory AsMemberOf(IGroup group)
    {
        _groups.Add(group);
        return this;
    }

    /// <summary>
    /// Enumerable of groups to which the user will be assigned.
    /// </summary>
    /// <param name="groups"></param>
    /// <returns></returns>
    public UserFactory AsMemberOf(IEnumerable<IGroup> groups)
    {
        _groups.AddRange(groups);
        return this;
    }

    /// <summary>
    /// Grant console access to the user.
    /// </summary>
    /// <returns></returns>
    public UserFactory HasConsoleAccess()
    {
        _password = new Secret(this, "secret-password", new SecretProps
        {
            SecretName = Cloudspace.ResourcePath(Props.UserName!, "Password"),
            Description = $"Username & password combination for console access for {Props.UserName}",
            GenerateSecretString = new SecretStringGenerator
            {
                SecretStringTemplate = JsonSerializer.Serialize(new Dictionary<string, string>
                {
                    { "username", Props.UserName! }
                }),
                GenerateStringKey = "password",
                PasswordLength = 16
            }
        });
        Props.Password = _password.SecretValueFromJson("password");

        return this;
    }

    /// <summary>
    /// Allows user programmatic access through CLI.
    /// </summary>
    /// <returns></returns>
    public UserFactory HasProgrammaticAccess()
    {
        _hasProgrammaticAccess = true;
        return this;
    }

    /// <summary>
    /// Constructs a secret key for a user a save it's value to the SSM parameter store.
    /// </summary>
    /// <param name="user"></param>
    private void ConstructSecretKeyForProgrammaticAccess(IUser user)
    {
        AccessKey accessKey = new(this, "access-key", new AccessKeyProps
        {
            User = user
        });
        Secret secret = new(this, "secret-access-key", new SecretProps
        {
            SecretName = Cloudspace.ResourcePath(user.UserName, "secretAccessKey"),
            Description = $"Secret key combination for programmatic access for {user.UserName}",
            SecretObjectValue = new Dictionary<string, SecretValue>
            {
                { "AccessKeyId", SecretValue.UnsafePlainText(accessKey.AccessKeyId) },
                { "SecretAccessKey", accessKey.SecretAccessKey }
            }
        });
        secret.GrantRead(user);
    }
}