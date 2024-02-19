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
    /// <remarks>
    /// If the repository is stored on another account, use <see cref="FromEcrArn"/> or <see cref="FromEcrRepositoryAttributes"/> instead.
    /// </remarks>
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

    /// <summary>
    /// Configure the usage of docker image from ECR, based on tag or digest.
    /// </summary>
    /// <remarks>
    /// Uses existing instance of the ECR repository.
    /// </remarks>
    /// <param name="repository"></param>
    /// <param name="tagOrDigest"></param>
    /// <returns></returns>
    public DockerImageLambdaFactory FromEcr(IRepository repository, string tagOrDigest)
    {
        Props.Code = DockerImageCode.FromEcr(repository, new EcrImageCodeProps
        {
            TagOrDigest = tagOrDigest
        });
        return this;
    }

    /// <summary>
    /// Configure the usage of docker image from ECR, based on tag or digest.
    /// </summary>
    /// <remarks>
    /// If the ARN is late-bound value (token), use <see cref="FromEcrRepositoryAttributes"/> instead.
    /// </remarks>
    /// <param name="repositoryArn"></param>
    /// <param name="tagOrDigest"></param>
    /// <returns></returns>
    public DockerImageLambdaFactory FromEcrArn(string repositoryArn, string tagOrDigest)
    {
        Props.Code = DockerImageCode.FromEcr(Repository.FromRepositoryArn(this, "image-source", repositoryArn), new EcrImageCodeProps
        {
            TagOrDigest = tagOrDigest
        });
        return this;
    }

    /// <summary>
    /// Configure the usage of docker image from ECR, based on tag or digest.
    /// </summary>
    /// <param name="attributes"></param>
    /// <param name="tagOrDigest"></param>
    /// <returns></returns>
    public DockerImageLambdaFactory FromEcrRepositoryAttributes(IRepositoryAttributes attributes, string tagOrDigest)
    {
        Props.Code = DockerImageCode.FromEcr(Repository.FromRepositoryAttributes(this, "image-source", attributes), new EcrImageCodeProps
        {
            TagOrDigest = tagOrDigest
        });
        return this;
    }
}