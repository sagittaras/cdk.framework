namespace Sagittaras.CDK.Framework;

/// <summary>
/// Static class helping to mark the resources used in the AWS.
/// </summary>
public static class Cloudspace
{
    /// <summary>
    /// ID of the AWS account to which the resources are assigned.
    /// </summary>
    public static string AccountId { get; set; } = string.Empty;

    /// <summary>
    /// The name of region in which the application is being deployed.
    /// </summary>
    public static string DefaultRegion { get; set; } = "us-east-1";

    /// <summary>
    /// Namespace marking the cloud resources.
    /// </summary>
    public static string Name { get; set; } = string.Empty;

    /// <summary>
    /// Generate a resource name prefixed with the namespace.
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    public static string ResourceName(string resource)
    {
        return $"{Name}_{resource}";
    }

    /// <summary>
    /// Generate a repository name prefixed with the namespace.
    /// </summary>
    /// <param name="repository"></param>
    /// <returns></returns>
    public static string RepositoryName(string repository)
    {
        return $"{Name}/{repository}".ToLower();
    }

    /// <summary>
    /// Generate a suitable name for the bucket prefixed with namespace.
    /// </summary>
    /// <param name="bucket"></param>
    /// <returns></returns>
    public static string BucketName(string bucket)
    {
        return $"{Name}-{bucket}".ToLower();
    }

    /// <summary>
    /// Creates a name of stack prefixed with the namespace.
    /// </summary>
    /// <param name="stack"></param>
    /// <returns></returns>
    public static string StackName(string stack)
    {
        return $"{Name}-{stack}";
    }

    /// <summary>
    /// Create a stack prefixed with the namespace.
    /// </summary>
    /// <param name="stack"></param>
    /// <returns></returns>
    public static string StackId(string stack)
    {
        return StackName(stack).ToLower();
    }

    /// <summary>
    /// Generate a path for resource prefixed with the namespace.
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    public static string ResourcePath(string resource)
    {
        return $"{Name}/{resource}";
    }

    /// <summary>
    /// Generate a domain path for resource prefixed with the namespace.
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="resource"></param>
    /// <returns></returns>
    public static string ResourcePath(string domain, string resource)
    {
        return ResourcePath($"{domain}/{resource}");
    }
}