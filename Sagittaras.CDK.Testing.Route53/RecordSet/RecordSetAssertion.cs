using Sagittaras.CDK.Testing.Resources;

namespace Sagittaras.CDK.Testing.Route53.RecordSet;

/// <summary>
/// Assertion for AWS::Route53::RecordSet.
/// </summary>
public class RecordSetAssertion : ResourceAssertion<RecordSetProperties>
{
    /// <inheritdoc />
    public override string Type => "AWS::Route53::RecordSet";
}