using Amazon.CDK;
using Amazon.CDK.AWS.Route53;
using Constructs;
using Sagittaras.CDK.Framework.Factory;

namespace Sagittaras.CDK.Framework.Route53;

/// <summary>
/// Construct factory for creating the public hosted zone.
/// </summary>
public partial class PublicHostedZoneFactory : ConstructFactory<PublicHostedZone, PublicHostedZoneProps>
{
    /// <summary>
    /// Default TTL value in seconds.
    /// </summary>
    private const int DefaultTtlInSeconds = 1800;

    /// <summary>
    /// Dictionary containing definitions of the records that will be assigned to hosted zone.
    /// </summary>
    private readonly Dictionary<string, IRecordSetOptions> _records = new();

    public PublicHostedZoneFactory(Construct scope, string domain, string comment) : base(scope, domain)
    {
        Props = new PublicHostedZoneProps
        {
            ZoneName = domain,
            Comment = comment
        };
    }

    /// <summary>
    /// Props of the hosted zone.
    /// </summary>
    public override PublicHostedZoneProps Props { get; }

    /// <inheritdoc />
    public override PublicHostedZone Construct()
    {
        PublicHostedZone zone = new(this, "public-zone", Props);
        InstantiateRecords(zone);
        EnableDnsSec(zone);

        return zone;
    }

    /// <summary>
    /// Adds a new record props to the hosted zone.
    /// </summary>
    /// <remarks>
    /// Records are created once the hosted zone is constructed.
    /// </remarks>
    /// <param name="id"></param>
    /// <param name="props"></param>
    /// <typeparam name="TRecordProps"></typeparam>
    /// <returns></returns>
    public PublicHostedZoneFactory AddRecordProps<TRecordProps>(string id, TRecordProps props)
        where TRecordProps : IRecordSetOptions
    {
        _records.Add(id, props);
        return this;
    }

    /// <summary>
    /// Returns existing TTL or creates a new one from default value.
    /// </summary>
    /// <param name="ttl"></param>
    /// <returns></returns>
    internal static Duration GetTtl(Duration? ttl)
    {
        return ttl ?? Duration.Seconds(DefaultTtlInSeconds);
    }

    /// <summary>
    /// Instantiate all records based on their props.
    /// </summary>
    /// <param name="zone"></param>
    private void InstantiateRecords(PublicHostedZone zone)
    {
        foreach ((string id, IRecordSetOptions props) in _records)
        {
            InstantiateRecord(id, props, zone);
        }
    }

    /// <summary>
    /// Instantiate a new record based on the props type.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="props"></param>
    /// <param name="zone"></param>
    /// <exception cref="ArgumentException"></exception>
    private void InstantiateRecord(string id, IRecordSetOptions props, IHostedZone zone)
    {
        switch (props)
        {
            case CnameRecordProps cnameProps:
                cnameProps.Zone = zone;
                _ = new CnameRecord(this, id, cnameProps);
                break;
            case MxRecordProps mxProps:
                mxProps.Zone = zone;
                _ = new MxRecord(this, id, mxProps);
                break;
            case TxtRecordProps txtProps:
                txtProps.Zone = zone;
                _ = new TxtRecord(this, id, txtProps);
                break;
            case NsRecordProps nsProps:
                nsProps.Zone = zone;
                _ = new NsRecord(this, id, nsProps);
                break;
            case DsRecordProps dsProps:
                dsProps.Zone = zone;
                _ = new DsRecord(this, id, dsProps);
                break;
            default:
                throw new ArgumentException($"Unknown record type: {props.GetType().Name}");
        }
    }
}