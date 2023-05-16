using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record Meter(
    [property: JsonPropertyName("eid")] long EId,                           // TODO: int? long? other?
    [property: JsonPropertyName("state")] string State,                     // TODO: Should be enum
    [property: JsonPropertyName("measurementType")] string MeasurementType, // TODO: Should be enum
    [property: JsonPropertyName("phaseMode")] string PhaseMode,
    [property: JsonPropertyName("phaseCount")] int PhaseCount,
    [property: JsonPropertyName("meteringStatus")] string MeteringStatus,   // TODO: Should be enum
    [property: JsonPropertyName("statusFlags")] string[] StatusFlags        // TODO: Which values can we expect here?
);
