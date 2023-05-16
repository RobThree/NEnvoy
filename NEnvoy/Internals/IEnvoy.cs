using NEnvoy.Internals.Models;
using NEnvoy.Models;
using Refit;

namespace NEnvoy.Internals;

internal interface IEnvoyXml
{
    [Get("/info.xml")]
    Task<EnvoyInfo> GetEnvoyInfoAsync(CancellationToken cancellationToken = default);
}

internal interface IEnvoyJson {
    [Get("/ivp/meters/reports/consumption")]
    Task<IEnumerable<ConsumptionReport>> GetConsumptionAsync(CancellationToken cancellationToken);

    [Get("/api/v1/production")]
    Task<V1Production> GetV1ProductionAsync(CancellationToken cancellationToken);

    [Get("/api/v1/production/inverters")]
    Task<IEnumerable<V1Inverter>> GetV1InvertersAsync(CancellationToken cancellationToken);

    [Get("/ivp/meters")]
    Task<IEnumerable<Meter>> GetMetersAsync(CancellationToken cancellationToken = default);

    [Get("/ivp/meters/readings")]
    Task<IEnumerable<RootMeterReading>> GetMeterReadingsAsync(CancellationToken cancellationToken = default);

    [Get("/admin/lib/wireless_display.json?site_info=0")]
    Task<WirelessDisplay> GetWirelessDisplayAsync(CancellationToken cancellationToken = default);

    [Get("/admin/lib/wireless_display.json?site_info=1")]
    Task<WirelessDisplayExtended> GetWirelessDisplayExtendedAsync(CancellationToken cancellationToken = default);

    [Get("/home.json")]
    Task<Home> GetHome(CancellationToken cancellationToken = default);
}
