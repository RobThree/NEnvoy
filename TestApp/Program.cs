using Microsoft.Extensions.Configuration;
using NEnvoy;
using NEnvoy.Models;

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

        var client = await GetClientAsync(config.Envoy, "token.txt").ConfigureAwait(false);

        //var deviceinfo = await client.GetEnvoyInfoAsync().ConfigureAwait(false);
        //var consumption = await client.GetConsumptionAsync().ConfigureAwait(false);
        //var v1production = await client.GetV1ProductionAsync().ConfigureAwait(false);
        //var v1inverters = await client.GetV1InvertersAsync().ConfigureAwait(false);
        //var meters = await client.GetMetersAsync().ConfigureAwait(false);
        //var meterreadings = await client.GetMeterReadingsAsync().ConfigureAwait(false);
        //var wireless = await client.GetWirelessDisplayAsync().ConfigureAwait(false);
        //var wirelessext = await client.GetWirelessDisplayExtendedAsync().ConfigureAwait(false);
        //var home = await client.GetHomeAsync().ConfigureAwait(false);
        //var production = await client.GetProductionAsync().ConfigureAwait(false);
        //var inventory = await client.GetInventoryAsync().ConfigureAwait(false);

        //var devicestatus = await client.GetDeviceStatusAsync().ConfigureAwait(false);
        //var inverterdata = devicestatus.PCU?
        //    .Where(v => v.Value["devType"].GetValue<int>() == 1)
        //    .ToDictionary(v => v.Key, v => new
        //    {
        //        Temp = v.Value["temperature"].GetValue<int>(),
        //        ReportDate = DateTimeOffset.FromUnixTimeSeconds(v.Value["reportDate"].GetValue<int>()).ToLocalTime(),
        //        DcVoltageIn = v.Value["dcVoltageINmV"].GetValue<int>() / 1000m,
        //        DcCurrentIn = v.Value["dcCurrentINmA"].GetValue<int>() / 1000m,
        //        AcVoltageIn = v.Value["acVoltageINmV"].GetValue<int>() / 1000m,
        //        AcPowerIn = v.Value["acPowerINmW"].GetValue<int>() / 1000m,
        //        Communicating = v.Value["communicating"].GetValue<bool>(),
        //        Recent = v.Value["recent"].GetValue<bool>(),
        //        Producing = v.Value["producing"].GetValue<bool>()
        //    });
    }

    private static async Task<IEnvoyClient> GetClientAsync(EnvoyConnectionInfo envoyConnectionInfo, string tokenfile, CancellationToken cancellationToken = default)
    {
        // If we don't have a session token, we create a client by logging in, else we create one from the session token
        if (!File.Exists(tokenfile))
        {
            var client = await EnvoyClient.FromLoginAsync(envoyConnectionInfo, cancellationToken).ConfigureAwait(false);
            await File.WriteAllTextAsync(tokenfile, client.GetToken(), cancellationToken).ConfigureAwait(false);
            return client;
        }
        return EnvoyClient.FromToken(await File.ReadAllTextAsync(tokenfile, cancellationToken).ConfigureAwait(false), envoyConnectionInfo);
    }
}