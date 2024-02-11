using Amazon.CDK.AWS.Cognito;

namespace Sagittaras.CDK.Framework.Cognito.UserPools;

public partial class UserPoolFactory
{
    /// <summary>
    ///     Dictionary containing definition of clients for the user-pool.
    /// </summary>
    private readonly Dictionary<string, UserPoolClientOptions> _clients = new();

    /// <summary>
    ///     Assign all predefined clients to the user-pool.
    /// </summary>
    /// <param name="pool"></param>
    private void AssignClientsToPool(IUserPool pool)
    {
        foreach ((string clientId, UserPoolClientOptions options) in _clients)
        {
            pool.AddClient(clientId, options);
        }
    }

    /// <summary>
    ///     Creates a new public client definition for the user-pool.
    /// </summary>
    public void AddPublicClient()
    {
        _clients.Add("public", new UserPoolClientOptions());
    }

    /// <summary>
    ///     Adds a new client definition to the user-pool.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="options"></param>
    public void AddClient(string id, UserPoolClientOptions options)
    {
        _clients.Add(id, options);
    }
}