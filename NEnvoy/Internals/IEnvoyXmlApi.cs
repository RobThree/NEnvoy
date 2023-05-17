using NEnvoy.Models;
using Refit;

namespace NEnvoy.Internals;

internal interface IEnvoyXmlApi
{
    [Get("/info.xml")]
    Task<EnvoyInfo> GetEnvoyInfoAsync(CancellationToken cancellationToken = default);
}
