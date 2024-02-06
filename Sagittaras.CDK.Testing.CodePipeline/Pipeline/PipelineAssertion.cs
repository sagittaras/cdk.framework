using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.CodePipeline.Pipeline;

/// <summary>
/// Assertion for AWS::CodePipeline::Pipeline.
/// </summary>
public class PipelineAssertion : ResourceAssertion<PipelineProperties>
{
    /// <inheritdoc />
    public override string Type => "AWS::CodePipeline::Pipeline";
}