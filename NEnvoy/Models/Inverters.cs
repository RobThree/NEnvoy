using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record Inverter(
    int ActiveCount,
    DateTimeOffset ReadingTime,
    [property: JsonPropertyName("wNow")] decimal WattsNow,
    [property: JsonPropertyName("whLifetime")] decimal WattHoursNow
) : ProductionRecord(ActiveCount, ReadingTime);
