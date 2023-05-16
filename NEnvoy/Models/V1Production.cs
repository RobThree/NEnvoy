using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record V1Production
(
    [property: JsonPropertyName("wattHoursToday")] decimal WattHoursToday,
    [property: JsonPropertyName("wattHoursSevenDays")] decimal WattHoursSevenDays,
    [property: JsonPropertyName("wattHoursLifetime")] decimal WattHoursLifetime,
    [property: JsonPropertyName("wattsNow")] decimal WattsNow
);
