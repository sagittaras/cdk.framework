using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Lambda.Function;

public class TracingConfigProperties : ResourceProperties
{
    public TracingConfigMode? Mode { get; set; }
}