using System.Text.Json.Serialization;

namespace NEnvoy.Models;
public record ProductionData
(
    [property: JsonPropertyName("production")] IEnumerable<ProductionRecord> Production,
    [property: JsonPropertyName("consumption")] IEnumerable<ProductionRecord> Consumption,
    [property: JsonPropertyName("storage")] IEnumerable<ProductionRecord> Storage
);