using Cranking.AI;

var ipResolver = new PublicIpResolver();

var myPublicIp = ipResolver.GetMyPublicIpAddress();

Console.WriteLine($"My public IP address is: {myPublicIp}");

var myPublicIpCountry = await PublicIpLocator.IpToCountryAsync(myPublicIp);
Console.WriteLine($"My public IP address is located in: {myPublicIpCountry?.CountryRegion?.IsoCode}");

foreach (var ip in new string[] { "97.103.171.106", "1.1.1.1", "8.8.8.8", "9.9.9.9", "8.8.8.8", "202.112.11.11", "195.149.238.1", "193.232.11.11", "212.192.11.11" })
{
   try
   {
      var ipLocation = await PublicIpLocator.IpToCountryAsync(ip);
      Console.WriteLine($"IP address {ip} is located in: {ipLocation?.CountryRegion?.IsoCode}");
   }
   catch (Exception ex)
   {
      Console.WriteLine($"Failed to locate IP address {ip}: {ex.Message}");
   }
}
