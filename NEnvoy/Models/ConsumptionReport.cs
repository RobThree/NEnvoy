using NEnvoy.Internals.Converters;
using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record ConsumptionReport
(
    [property: JsonPropertyName("createdAt")][property: JsonConverter(typeof(IntTimestampDateTimeOffsetJsonConverter))] DateTimeOffset CreatedAt,
    [property: JsonPropertyName("reportType")] string ReportType,
    [property: JsonPropertyName("cumulative")] ConsumptionValues Cumulative,
    [property: JsonPropertyName("lines")] ConsumptionValues[] Lines

);
