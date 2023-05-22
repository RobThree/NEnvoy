using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record Site(
    [property: JsonPropertyName("is_current_ssid")] bool IsCurrentSSId,
    [property: JsonPropertyName("mac")] string MacAddress,
    [property: JsonPropertyName("channel")] string Channel,
    [property: JsonPropertyName("ssid")] string SSID,
    [property: JsonPropertyName("bars")] int Bars,
    [property: JsonPropertyName("secured")] bool Secured,
    [property: JsonPropertyName("wps")] bool WPS,
    [property: JsonPropertyName("unsupported")] bool Unsupported,
    [property: JsonPropertyName("security_mode")] string SecurityMode,
    [property: JsonPropertyName("encryption_type")] string EncryptionType   //TODO: Enum??
);