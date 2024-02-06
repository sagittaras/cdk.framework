using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Lambda.Function;

public class EnvironmentProperties : ResourceProperties
{
    public Dictionary<string, string>? Variables { get; set; }
}