using Microsoft.Extensions.Configuration;
using NEnvoy;

namespace TestApp;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var configprovider = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>()
            .Build();

        var config = new Configuration();
        configprovider.Bind(config);

        // If we don't have a session token, we create a client by logging in, else we create one from the session token
        var client = string.IsNullOrEmpty(config.Session?.Token)
            ? await EnvoyClient.FromLoginAsync(config.Envoy).ConfigureAwait(false)
            : EnvoyClient.FromSessionToken(config.Envoy.EnvoyHost, config.Session.Token, config.Session.IsConsumer);

        //var deviceinfo = await client.GetEnvoyInfoAsync().ConfigureAwait(false);
        //var consumption = await client.GetConsumptionAsync().ConfigureAwait(false);
        //var v1production = await client.GetV1ProductionAsync().ConfigureAwait(false);
        //var v1inverters = await client.GetV1InvertersAsync().ConfigureAwait(false);
        //var meters = await client.GetMetersAsync().ConfigureAwait(false);
        //var meterreadings = await client.GetMeterReadingsAsync().ConfigureAwait(false);
        //var wireless = await client.GetWirelessDisplayAsync().ConfigureAwait(false);        
        //var wirelessext = await client.GetWirelessDisplayExtendedAsync().ConfigureAwait(false);
        //var home = await client.GetHome().ConfigureAwait(false);
    }
}
