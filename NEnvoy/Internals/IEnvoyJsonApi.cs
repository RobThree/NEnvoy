using NEnvoy.Internals.Models;
using NEnvoy.Models;
using Refit;

namespace NEnvoy.Internals;

internal interface IEnvoyJsonApi
{

    [Get("/ivp/meters/reports/consumption")]
    Task<IEnumerable<ConsumptionReport>> GetConsumptionAsync(CancellationToken cancellationToken = default);

    [Get("/api/v1/production")]
    Task<V1Production> GetV1ProductionAsync(CancellationToken cancellationToken = default);

    [Get("/api/v1/production/inverters")]
    Task<IEnumerable<V1Inverter>> GetV1InvertersAsync(CancellationToken cancellationToken = default);

    [Get("/ivp/meters")]
    Task<IEnumerable<Meter>> GetMetersAsync(CancellationToken cancellationToken = default);

    [Get("/ivp/meters/readings")]
    Task<IEnumerable<RootMeterReading>> GetMeterReadingsAsync(CancellationToken cancellationToken = default);

    [Get("/ivp/peb/devstatus")]
    Task<IVPDeviceStatus> GetDeviceStatusAsync(CancellationToken cancellationToken = default);

    [Get("/admin/lib/wireless_display.json?site_info=0")]
    Task<WirelessDisplay> GetWirelessDisplayAsync(CancellationToken cancellationToken = default);

    [Get("/admin/lib/wireless_display.json?site_info=1")]
    Task<WirelessDisplayExtended> GetWirelessDisplayExtendedAsync(CancellationToken cancellationToken = default);

    [Get("/production.json?details=1")]
    Task<ProductionData> GetProductionAsync(CancellationToken cancellationToken = default);

    [Get("/inventory.json")]
    Task<IEnumerable<InventoryItem>> GetInventoryAsync(CancellationToken cancellationToken = default);

    [Get("/home.json")]
    Task<Home> GetHomeAsync(CancellationToken cancellationToken = default);
}
