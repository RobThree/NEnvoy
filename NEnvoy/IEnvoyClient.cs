using NEnvoy.Internals.Models;
using NEnvoy.Models;

namespace NEnvoy;
public interface IEnvoyClient
{
    bool IsConsumer { get; }
    string SessionToken { get; }

    Task<EnvoyInfo> GetEnvoyInfoAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ConsumptionReport>> GetConsumptionAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<V1Inverter>> GetV1InvertersAsync(CancellationToken cancellationToken = default);
    Task<V1Production> GetV1ProductionAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Meter>> GetMetersAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<RootMeterReading>> GetMeterReadingsAsync(CancellationToken cancellationToken = default);
    Task<WirelessDisplay> GetWirelessDisplayAsync(CancellationToken cancellationToken = default);
    Task<WirelessDisplayExtended> GetWirelessDisplayExtendedAsync(CancellationToken cancellationToken = default);
    Task<Home> GetHome(CancellationToken cancellationToken = default);
}