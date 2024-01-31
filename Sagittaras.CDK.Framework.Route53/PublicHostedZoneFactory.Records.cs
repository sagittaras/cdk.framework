using Amazon.CDK;
using Amazon.CDK.AWS.Route53;

namespace Sagittaras.CDK.Framework.Route53;

/// <summary>
/// Factory API for defining different record types for hosted zone.
/// </summary>
public partial class PublicHostedZoneFactory
{
    /// <summary>
    /// Adds a new TXT record to the hosted zone.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="recordName"></param>
    /// <param name="values"></param>
    /// <param name="comment"></param>
    /// <param name="ttl"></param>
    /// <returns></returns>
    public PublicHostedZoneFactory AddTxtRecord(string id, string[] values, string? recordName = null, string? comment = null, Duration? ttl = null)
    {
        return AddRecordProps(id, new TxtRecordProps
        {
            RecordName = recordName,
            Comment = comment,
            Ttl = GetTtl(ttl),
            Values = values
        });
    }

    /// <summary>
    /// Adds TXT record with single value.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="value"></param>
    /// <param name="recordName"></param>
    /// <param name="comment"></param>
    /// <param name="ttl"></param>
    /// <returns></returns>
    public PublicHostedZoneFactory AddTxtRecord(string id, string value, string? recordName = null, string? comment = null, Duration? ttl = null)
    {
        return AddRecordProps(id, new TxtRecordProps
        {
            RecordName = recordName,
            Comment = comment,
            Ttl = GetTtl(ttl),
            Values = new[] { value }
        });
    }

    /// <summary>
    /// Adds a new CNAME record to the hosted zone.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="recordName"></param>
    /// <param name="domainName"></param>
    /// <param name="comment"></param>
    /// <param name="ttl"></param>
    /// <returns></returns>
    public PublicHostedZoneFactory AddCnameRecord(string id, string recordName, string domainName, string? comment = null, Duration? ttl = null)
    {
        return AddRecordProps(id, new CnameRecordProps
        {
            RecordName = recordName,
            Comment = comment,
            Ttl = GetTtl(ttl),
            DomainName = domainName
        });
    }

    /// <summary>
    /// Adds a new MX record to the hosted zone.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="values"></param>
    /// <param name="comment"></param>
    /// <param name="recordName"></param>
    /// <param name="ttl"></param>
    /// <returns></returns>
    public PublicHostedZoneFactory AddMxRecord(string id, IMxRecordValue[] values, string comment, string? recordName = null, Duration? ttl = null)
    {
        return AddRecordProps(id, new MxRecordProps
        {
            RecordName = recordName,
            Comment = comment,
            Ttl = GetTtl(ttl),
            Values = values
        });
    }

    /// <summary>
    /// Adds a new NS record to the hosted zone.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="values"></param>
    /// <param name="recordName"></param>
    /// <param name="comment"></param>
    /// <param name="ttl"></param>
    /// <returns></returns>
    public PublicHostedZoneFactory AddNsRecord(string id, string[] values, string? recordName = null, string? comment = null, Duration? ttl = null)
    {
        return AddRecordProps(id, new NsRecordProps
        {
            RecordName = recordName,
            Comment = comment,
            Ttl = GetTtl(ttl),
            Values = values
        });
    }

    /// <summary>
    /// Adds a new DS record to the hosted zone.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="recordName"></param>
    /// <returns></returns>
    public PublicHostedZoneFactory AddDsRecord(string id, string recordName)
    {
        return AddRecordProps(id, new DsRecordProps
        {
            RecordName = recordName,
            Comment = "",
            Values = new[] { "" },
            Ttl = GetTtl(null)
        });
    }
}