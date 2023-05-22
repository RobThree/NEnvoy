using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record Alert
(
    [property: JsonPropertyName("msg_key")] string Key
);