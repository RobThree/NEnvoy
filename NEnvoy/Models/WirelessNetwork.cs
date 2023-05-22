using NEnvoy.Internals.Converters;
using System.Net;
using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record WirelessNetwork(
  [property: JsonPropertyName("ssid")] string SSId,
  [property: JsonPropertyName("status")] string Status,
  [property: JsonPropertyName("ip_address")][property: JsonConverter(typeof(IPAddressJsonConverter))] IPAddress? IPAddress,
  [property: JsonPropertyName("gateway_ip")][property: JsonConverter(typeof(IPAddressJsonConverter))] IPAddress? GatewayIP,
  [property: JsonPropertyName("security_mode")] string SecurityMode,        // TODO: Enum???
  [property: JsonPropertyName("encryption_type")] string EncryptionType,    // TODO: Enum???
  [property: JsonPropertyName("ap_bssid")] int? APBSSId,
  [property: JsonPropertyName("channel")] int? Channel,
  [property: JsonPropertyName("bars")] int? Bars
);
