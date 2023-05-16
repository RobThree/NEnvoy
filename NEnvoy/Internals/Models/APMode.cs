using System.Text.Json.Serialization;

namespace NEnvoy.Internals.Models;

public record APMode(
  [property: JsonPropertyName("enabled")] bool Enabled,
  [property: JsonPropertyName("name")] string Name
);
