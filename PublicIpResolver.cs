using System.Net;
using DnsClient;

namespace Cranking.AI;

public class PublicIpResolver
{
    private readonly LookupClient _dnsClient;
    private const string OpenDnsResolverDomain = "resolver1.opendns.com";
    private const string MyIpDomain = "myip.opendns.com";

    public PublicIpResolver()
    {
        // Use DNS client to resolve the OpenDNS resolver (i.e., lookup IP of OpenDnsResolverDomain)
        var tempDnsClient = new LookupClient();
        var resolverResult = tempDnsClient.Query(OpenDnsResolverDomain, QueryType.A);
        
        if (!resolverResult.Answers.ARecords().Any())
        {
            throw new InvalidOperationException($"Failed resolving {OpenDnsResolverDomain}");
        }

        var resolverIp = resolverResult.Answers.ARecords().First().Address;
        var endpoint = new IPEndPoint(resolverIp, 53);
        _dnsClient = new LookupClient(endpoint);
    }

    public string GetMyPublicIpAddress()
    {
        var result = _dnsClient.Query(MyIpDomain, QueryType.A);
        
        if (!result.Answers.ARecords().Any())
        {
            throw new InvalidOperationException("Failed resolving public IP address");
        }

        return result.Answers.ARecords().First().Address.ToString();
    }
}
