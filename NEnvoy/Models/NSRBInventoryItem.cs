using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record NSRBInventoryItem(
    [property: JsonPropertyName("devices")] IEnumerable<NSRBDevice> Devices
) : InventoryItem();
