using NEnvoy.Internals.Models;
using Refit;

namespace NEnvoy.Internals;

internal interface IEnphase
{
    [Post("/login/login.json")]
    Task<EnphaseLoginResponse> LoginAsync([Body(BodySerializationMethod.UrlEncoded)] EnphaseLoginRequest login, CancellationToken cancellationToken = default);
}
