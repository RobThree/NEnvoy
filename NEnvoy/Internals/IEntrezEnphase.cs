using NEnvoy.Internals.Models;
using Refit;

namespace NEnvoy.Internals;

internal interface IEntrezEnphase
{
    [Post("/tokens")]
    Task<string> RequestTokenAsync(EnphaseTokenRequest tokenRequest, CancellationToken cancellationToken = default);
}
