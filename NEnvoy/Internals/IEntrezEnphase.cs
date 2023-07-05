using NEnvoy.Internals.Models;
using Refit;

namespace NEnvoy.Internals;

internal interface IEntrezEnphase
{
    [Post("/tokens")]
    Task<string> RequestTokenAsync(EntrezEnphaseTokenRequest tokenRequest, CancellationToken cancellationToken = default);

    [Post("/login")]
    Task<HttpResponseMessage> LoginAsync([Body(BodySerializationMethod.UrlEncoded)] EntrezEnphaseLoginRequest loginRequest, CancellationToken cancellationToken = default);
}
