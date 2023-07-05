using System.Text.Json.Serialization;

namespace NEnvoy.Internals.Models;

internal record JWTResponse
(
    [property: JsonPropertyName("access_token")] string AccessToken
);