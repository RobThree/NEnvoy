using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record Inverters(
    int ActiveCount,
    DateTimeOffset ReadingTime,
    [property: JsonPropertyName("wNow")] decimal WattsNow,
    [property: JsonPropertyName("whLifetime")] decimal WattHoursNow
) : ProductionRecord(ActiveCount, ReadingTime);
