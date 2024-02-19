using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Abstraction;
using Sagittaras.CDK.Framework.CodeBuild.BuildSpecification.Factory.Sections;

namespace Sagittaras.CDK.Framework.Amplify.BuildSpecification.Factory;

/// <summary>
/// Describes the phases section of the amplify application stages.
/// </summary>
public class AmplifyPhasesSection : PhasesSection
{
    public AmplifyPhasesSection()
    {
        RewriteSectionName(BuildPhase.PreBuild, "preBuild");
        RewriteSectionName(BuildPhase.PostBuild, "postBuild");
    }

    /// <inheritdoc />
    public override IBuildPhaseSection Phase(BuildPhase phase)
    {
        if (phase == BuildPhase.Install)
        {
            throw new NotSupportedException("Amplify does not support the install phase.");
        }

        return base.Phase(phase);
    }
}