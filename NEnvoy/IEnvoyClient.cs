using NEnvoy.Models;

namespace NEnvoy;
public interface IEnvoyClient
{
    bool IsConsumer { get; }
    string SessionToken { get; }

    Task<EnvoyInfo> GetEnvoyInfoAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ConsumptionReport>> GetConsumptionAsync(CancellationToken cancellationToken = default);
}