using Amazon.CDK.AWS.ECR;
using Amazon.CDK.AWS.Lambda;
using Constructs;

namespace Sagittaras.CDK.Framework.Lambda;

/// <summary>
/// Factory creating a Lambda function with usage of Docker Image.
/// </summary>
public class DockerImageLambdaFactory : LambdaFactory<DockerImageFunction, DockerImageFunctionProps>
{
    public DockerImageLambdaFactory(Construct scope, string functionName) : base(scope, functionName)
    {
    }

    /// <inheritdoc />
    public override DockerImageFunctionProps Props { get; } = new();

    /// <inheritdoc />
    public override DockerImageFunction Construct()
    {
        MapCommonProperties(Props);
        return new DockerImageFunction(this, "function", Props);
    }

    /// <summary>
    /// Configure the usage of docker image from ECR, based on tag or digest.
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="tagOrDigest"></param>
    /// <returns></returns>
    public DockerImageLambdaFactory FromEcr(string repository, string tagOrDigest)
    {
        Props.Code = DockerImageCode.FromEcr(Repository.FromRepositoryName(this, "image-source", repository), new EcrImageCodeProps
        {
            TagOrDigest = tagOrDigest
        });
        return this;
    }
}