using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record APMode(
  [property: JsonPropertyName("enabled")] bool Enabled,
  [property: JsonPropertyName("name")] string Name
);
