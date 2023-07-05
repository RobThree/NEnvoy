using System.Text.Json.Serialization;

namespace NEnvoy.Internals.Models;

internal record JWTRequest
(
    [property: JsonPropertyName("client_id")] string ClientId,
    [property: JsonPropertyName("grant_type")] string GrantType,
    [property: JsonPropertyName("redirect_uri")] string RedirectUri,
    [property: JsonPropertyName("code_verifier")] string CodeVerifier,
    [property: JsonPropertyName("code")] string Code
);