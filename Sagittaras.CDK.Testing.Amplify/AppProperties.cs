using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Amplify;

/// <summary>
/// Properties for AWS::Amplify::App.
/// </summary>
public class AppProperties: ResourceProperties
{
    /// <summary>
    /// Name of the Amplify App.
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Link to the repository for the Amplify App.
    /// </summary>
    public string? Repository { get; set; }
    
    /// <summary>
    /// Is the Amplify application static or is it using server-side rendering.
    /// </summary>
    public string? Platform { get; set; }
}