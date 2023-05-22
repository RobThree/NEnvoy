using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record DeviceControl
(
    [property: JsonPropertyName("gficlearset")] bool GfiClearSet
);