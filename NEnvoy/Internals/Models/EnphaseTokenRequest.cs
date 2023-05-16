using System.Text.Json.Serialization;

namespace NEnvoy.Internals.Models;

internal record EnphaseTokenRequest(
    [property: JsonPropertyName("session_id")] string SessionId,
    [property: JsonPropertyName("serial_num")] string Serial,
    [property: JsonPropertyName("username")] string Username
);
