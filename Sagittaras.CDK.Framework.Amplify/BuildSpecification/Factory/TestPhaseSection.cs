using Sagittaras.CDK.Framework.Amplify.BuildSpecification.Abstraction;
using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;
using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Factory.Sections;

namespace Sagittaras.CDK.Framework.Amplify.BuildSpecification.Factory;

/// <summary>
/// Defines a single phase for the test section.
/// </summary>
public class TestPhaseSection : BuildPhaseSection, ITestPhaseSection
{
    public TestPhaseSection(string sectionName) : base(sectionName)
    {
    }

    /// <inheritdoc />
    public override IBuildPhaseSection Finally(string command)
    {
        throw new NotSupportedException("Amplify test section does not support finally commands.");
    }

    /// <inheritdoc />
    public override IBuildPhaseSection OnFailure(FailureBehaviour failureBehaviour)
    {
        throw new NotSupportedException("Amplify test section does not support failure behaviour.");
    }
}