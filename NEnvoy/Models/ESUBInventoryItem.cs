using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record ESUBInventoryItem(
    [property: JsonPropertyName("devices")] IEnumerable<ESUBDevice> Devices
) : InventoryItem();
