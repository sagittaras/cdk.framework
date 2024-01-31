using Amazon.CDK.AWS.Route53;
using Constructs;
using Sagittaras.CDK.Framework.Extensions;

namespace Sagittaras.CDK.Framework.Route53.Extensions;

public static class HostedZoneExtension
{
    /// <summary>
    /// Adds child zone to the parent - Assigning NS records to the parent zone.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="scope"></param>
    /// <param name="child"></param>
    public static void AddChildZone(this IHostedZone parent, Construct scope, IHostedZone child)
    {
        _ = new NsRecord(scope, $"{child.ZoneName.ToResourceId()}-ns", new NsRecordProps
        {
            RecordName = child.ZoneName,
            Zone = parent,
            Values = child.HostedZoneNameServers!
                .OrderBy(x => x)
                .Select(x => x.TrimEnd('.'))
                .ToArray()
        });
    }

    /// <summary>
    /// Adds a DS record of the child zone to establish chain of trust for DNSSEC.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="scope"></param>
    /// <param name="child"></param>
    /// <param name="value"></param>
    public static void AddChildDsRecord(this IHostedZone parent, Construct scope, IHostedZone child, string value)
    {
        _ = new DsRecord(scope, $"{child.ZoneName.ToResourceId()}-ds", new DsRecordProps
        {
            RecordName = child.ZoneName,
            Zone = parent,
            Values = new[] { value }
        });
    }
}