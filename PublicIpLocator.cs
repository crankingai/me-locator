using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;

// Keep an eye on: https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/maps/Azure.Maps.Search/README.md

namespace Cranking.AI;

public class PublicIpLocator
{
   public static async Task<AzureMapLocation> IpToCountryAsync(string ip)
   {
      string? AzureMapApiKey = Environment.GetEnvironmentVariable("AZURE_MAP_KEY");
      if (string.IsNullOrEmpty(AzureMapApiKey))
      {
         throw new InvalidOperationException("Please provide an Azure Maps key in the environment variable AZURE_MAP_KEY");
      }

      var url = $"https://atlas.microsoft.com/geolocation/ip/json?subscription-key={AzureMapApiKey}&api-version=1.0&ip={ip}";
      using var client = new HttpClient();
      var result = await client.GetAsync(url);

      if (result.IsSuccessStatusCode)
      {
         var jsonString = await result.Content.ReadAsStringAsync();

         var location = JsonSerializer.Deserialize<AzureMapLocation>(jsonString);
         if (location == null)
         {
            throw new InvalidOperationException($"Failed to deserialize location for IP {ip}");
         }
         return location;
      }

      throw new InvalidOperationException($"Failed to get location for IP {ip}. Status: {result.StatusCode}");
   }


   // Azure Maps API supports locating an IP by country (ISO-2 CC).
   // State and city are not supported.
   // However, detailed location is supported for a waypoint.
   public static async Task<AzureMapLocation> GetLocationFromIP(string ipAddress)
   {
      return await IpToCountryAsync(ipAddress);
   }
}