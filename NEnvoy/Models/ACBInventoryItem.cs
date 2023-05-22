using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record ACBInventoryItem(
    [property: JsonPropertyName("devices")] IEnumerable<ACBDevice> Devices
) : InventoryItem();
