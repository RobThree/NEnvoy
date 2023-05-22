using NEnvoy.Internals.Converters;
using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record V1Inverter(
    [property: JsonPropertyName("serialNumber")] string Serial,
    [property: JsonPropertyName("lastReportDate")][property: JsonConverter(typeof(IntTimestampDateTimeOffsetJsonConverter))] DateTimeOffset LastReportDate,
    [property: JsonPropertyName("devType")] int DeviceType,                 // TODO: Should be enum
    [property: JsonPropertyName("lastReportWatts")] decimal LastReportedWatts,
    [property: JsonPropertyName("maxReportWatts")] decimal MaximumReportedWatts
);
