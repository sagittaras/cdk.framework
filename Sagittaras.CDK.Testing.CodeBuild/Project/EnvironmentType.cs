using Sagittaras.CDK.Framework.Enums;

namespace Sagittaras.CDK.Testing.CodeBuild.Project;

public enum EnvironmentType
{
    [CdkValue("LINUX_CONTAINER")]
    LinuxContainer,
    
    [CdkValue("ARM_CONTAINER")]
    ArmContainer,
    
    [CdkValue("LINUX_GPU_CONTAINER")]
    LinuxGpuContainer,
    
    [CdkValue("WINDOWS_CONTAINER")]
    WindowsContainer
}