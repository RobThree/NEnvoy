using NEnvoy.Models;
using Refit;

namespace NEnvoy.Internals;

internal interface IEnvoy
{
    [Get("/info.xml")]
    Task<EnvoyInfo> GetDeviceInfoAsync(CancellationToken cancellationToken = default);

    [Get("/ivp/meters/reports/consumption")]
    Task<IEnumerable<ConsumptionReport>> GetConsumptionAsync(CancellationToken cancellationToken);
}
