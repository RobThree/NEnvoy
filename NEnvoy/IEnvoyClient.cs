using NEnvoy.Models;

namespace NEnvoy;
public interface IEnvoyClient
{
    string GetToken();
    Task<EnvoyInfo> GetEnvoyInfoAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ConsumptionReport>> GetConsumptionAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<V1Inverter>> GetV1InvertersAsync(CancellationToken cancellationToken = default);
    Task<V1Production> GetV1ProductionAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Meter>> GetMetersAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<RootMeterReading>> GetMeterReadingsAsync(CancellationToken cancellationToken = default);
    Task<DeviceStatus> GetDeviceStatusAsync(IEqualityComparer<string>? equalityComparer = null, CancellationToken cancellationToken = default);
    Task<WirelessDisplay> GetWirelessDisplayAsync(CancellationToken cancellationToken = default);
    Task<WirelessDisplayExtended> GetWirelessDisplayExtendedAsync(CancellationToken cancellationToken = default);
    Task<ProductionData> GetProductionAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<InventoryItem>> GetInventoryAsync(CancellationToken cancellationToken = default);
    Task<Home> GetHomeAsync(CancellationToken cancellationToken = default);
}