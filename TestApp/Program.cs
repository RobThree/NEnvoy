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

        var sessionfile = "session.json";

        // If we don't have a session token, we create a client by logging in, else we create one from the session token
        var client = File.Exists(sessionfile)
            ? EnvoyClient.FromSession(await EnvoyClient.LoadSessionAsync(sessionfile).ConfigureAwait(false), config.Envoy.EnvoyHost)
            : await EnvoyClient.FromLoginAsync(config.Envoy).ConfigureAwait(false);

        await client.SaveSessionAsync(sessionfile).ConfigureAwait(false);

        // var deviceinfo = await client.GetEnvoyInfoAsync().ConfigureAwait(false);
        // var consumption = await client.GetConsumptionAsync().ConfigureAwait(false);
        // var v1production = await client.GetV1ProductionAsync().ConfigureAwait(false);
        // var v1inverters = await client.GetV1InvertersAsync().ConfigureAwait(false);
        // var meters = await client.GetMetersAsync().ConfigureAwait(false);
        // var meterreadings = await client.GetMeterReadingsAsync().ConfigureAwait(false);
        // var wireless = await client.GetWirelessDisplayAsync().ConfigureAwait(false);        
        // var wirelessext = await client.GetWirelessDisplayExtendedAsync().ConfigureAwait(false);
        // var home = await client.GetHomeAsync().ConfigureAwait(false);
        // var production = await client.GetProductionAsync().ConfigureAwait(false);
        // var inventory = await client.GetInventoryAsync().ConfigureAwait(false);

        var devicestatus = await client.GetDeviceStatusAsync().ConfigureAwait(false);
        var inverterdata = devicestatus.PCU?
            .Where(v => v.Value["devType"].GetValue<int>() == 1)
            .ToDictionary(v => v.Key, v => new
            {
                Temp = v.Value["temperature"].GetValue<int>(),
                ReportDate = DateTimeOffset.FromUnixTimeSeconds(v.Value["reportDate"].GetValue<int>()).ToLocalTime(),
                DcVoltageIn = v.Value["dcVoltageINmV"].GetValue<int>() / 1000m,
                DcCurrentIn = v.Value["dcCurrentINmA"].GetValue<int>() / 1000m,
                AcVoltageIn = v.Value["acVoltageINmV"].GetValue<int>() / 1000m,
                AcCurrentIn = v.Value["acPowerINmW"].GetValue<int>() / 1000m
            });
    }
}