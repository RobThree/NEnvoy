using System.Text.Json.Serialization;

namespace NEnvoy.Internals.Models;

internal record EnphaseLoginResponse
(
    [property: JsonPropertyName("message")] string Message,
    [property: JsonPropertyName("session_id")] string SessionId,
    [property: JsonPropertyName("manager_token")] string ManagerToken,
    [property: JsonPropertyName("is_consumer")] bool IsConsumer
);
