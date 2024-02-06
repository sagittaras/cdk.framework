using Amazon.CDK.AWS.CodeDeploy;
using Amazon.CDK.AWS.Lambda;
using Sagittaras.CDK.Framework.CodeDeploy.Deployments;

namespace Sagittaras.CDK.Framework.Lambda.Extensions;

/// <summary>
///     Helps to construct deployment for Lambda function.
/// </summary>
public static class FunctionDeploymentExtension
{
    /// <summary>
    ///     Creates a new deployment group for the function.
    /// </summary>
    /// <param name="function">Instance of function for which the deployment group is created.</param>
    /// <param name="groupName">Name of the deployment group.</param>
    /// <returns></returns>
    public static LambdaDeploymentGroupFactory HasDeployment(this Function function, string groupName)
    {
        return new LambdaDeploymentGroupFactory(function, groupName, function);
    }

    /// <summary>
    ///     Creates a new deployment group for the function as a part of the CodeDeploy application.
    /// </summary>
    /// <param name="function"></param>
    /// <param name="groupName"></param>
    /// <param name="codeDeployApplication"></param>
    /// <returns></returns>
    public static LambdaDeploymentGroupFactory HasDeployment(this Function function, string groupName, string codeDeployApplication)
    {
        return new LambdaDeploymentGroupFactory(function, groupName, function)
                .PartOf(codeDeployApplication)
            ;
    }

    /// <summary>
    ///     Creates a new deployment group for the function as a part of the CodeDeploy application.
    /// </summary>
    /// <param name="function"></param>
    /// <param name="groupName"></param>
    /// <param name="codeDeployApplication"></param>
    /// <returns></returns>
    public static LambdaDeploymentGroupFactory HasDeployment(this Function function, string groupName, ILambdaApplication codeDeployApplication)
    {
        return new LambdaDeploymentGroupFactory(function, groupName, function)
                .PartOf(codeDeployApplication)
            ;
    }
}