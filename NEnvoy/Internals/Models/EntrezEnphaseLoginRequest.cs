using System.Text.Json.Serialization;

namespace NEnvoy.Internals.Models;

internal record EntrezEnphaseLoginRequest(
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("password")] string Password,
    [property: JsonPropertyName("codeChallenge")] string CodeChallenge,
    [property: JsonPropertyName("redirectUri")] string RedirectUri,
    [property: JsonPropertyName("client")] string Client,
    [property: JsonPropertyName("clientId")] string ClientId,
    [property: JsonPropertyName("authFlow")] string AuthFlow,
    [property: JsonPropertyName("serialNum")] string SerialNumber,
    [property: JsonPropertyName("granttype")] string GrantType,
    [property: JsonPropertyName("state")] string State,
    [property: JsonPropertyName("invalidSerialNum")] string InvalidSerialNumber
);