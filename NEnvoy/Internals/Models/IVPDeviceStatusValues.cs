using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace NEnvoy.Internals.Models;

internal record IVPDeviceStatusValues
(
    [property: JsonPropertyName("fields")] string[] Fields,
    [property: JsonPropertyName("values")] JsonArray[] DeviceValues
);
