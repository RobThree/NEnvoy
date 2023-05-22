using NEnvoy.Internals.Converters;
using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record Network
(
    [property: JsonPropertyName("web_comm")] bool WebCommunication,
    [property: JsonPropertyName("ever_reported_to_enlighten")] bool EverReportedToEnlighten,
    [property: JsonPropertyName("last_enlighten_report_time")][property: JsonConverter(typeof(IntTimestampDateTimeOffsetJsonConverter))] DateTimeOffset LastEnlightenReportTime,
    [property: JsonPropertyName("primary_interface")] string PrimaryInterface,
    [property: JsonPropertyName("interfaces")] IEnumerable<NetworkInterface> Interfaces
);
