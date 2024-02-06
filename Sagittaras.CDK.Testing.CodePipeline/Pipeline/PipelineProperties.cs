using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.CodePipeline.Pipeline;

/// <summary>
/// Properties for AWS::CodePipeline::Pipeline.
/// </summary>
public class PipelineProperties : ResourceProperties
{
    public string? Name { get; set; }
    public PipelineType? Type { get; set; }
}