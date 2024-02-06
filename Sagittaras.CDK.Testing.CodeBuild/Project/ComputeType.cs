using Sagittaras.CDK.Framework.Enums;

namespace Sagittaras.CDK.Testing.CodeBuild.Project;

public enum ComputeType
{
    [CdkValue("BUILD_GENERAL1_SMALL")]
    General1Small,
    
    [CdkValue("BUILD_GENERAL1_MEDIUM")]
    General1Medium,
    
    [CdkValue("BUILD_GENERAL1_LARGE")]
    General1Large
}