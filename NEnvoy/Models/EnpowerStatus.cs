using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record EnpowerStatus
(
    [property: JsonPropertyName("connected")] bool Connected,
    [property: JsonPropertyName("grid_status")] string GridStatus   // TODO: Enum??
);
