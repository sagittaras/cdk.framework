using Sagittaras.CDK.Framework.Enums;

namespace Sagittaras.CDK.Testing.CodeDeploy.Application;

public enum ComputePlatform
{
    Server,
    Lambda,

    [CdkValue("ECS")] ElasticContainerService
}