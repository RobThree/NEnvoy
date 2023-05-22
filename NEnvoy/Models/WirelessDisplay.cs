using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record WirelessDisplay(
  [property: JsonPropertyName("supported")] bool Supported,
  [property: JsonPropertyName("present")] bool Present,
  [property: JsonPropertyName("configured")] bool Configured,
  [property: JsonPropertyName("up")] bool Up,
  [property: JsonPropertyName("carrier")] bool Carrier,
  [property: JsonPropertyName("current_network")] WirelessNetwork CurrentNetwork,
  [property: JsonPropertyName("device_info")] DeviceInfo DeviceInfo,
  [property: JsonPropertyName("selected_region")] string SelectedRegion,
  [property: JsonPropertyName("regions")] IEnumerable<string> Regions
);
