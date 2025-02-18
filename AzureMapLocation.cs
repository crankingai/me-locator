// DTO for the Azure Map API that determines the country of an IP address.
// IPv4 only right now.

using System.Text.Json.Serialization;

namespace Cranking.AI;

public class CountryRegion
{
   [JsonPropertyName("isoCode")]
   public string? IsoCode { get; set; }
}

public class AzureMapLocation
{
   [JsonPropertyName("countryRegion")]
   public CountryRegion? CountryRegion { get; set; }

   [JsonPropertyName("ipAddress")]
   public string? IpAddress { get; set; }
}
