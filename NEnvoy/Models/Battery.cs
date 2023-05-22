using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record Battery(
    int ActiveCount,
    DateTimeOffset ReadingTime,
    [property: JsonPropertyName("wNow")] decimal WattsNow,
    [property: JsonPropertyName("whNow")] decimal WattHoursNow,
    [property: JsonPropertyName("state")] string State // TODO: Enum??
) : ProductionRecord(ActiveCount, ReadingTime);
