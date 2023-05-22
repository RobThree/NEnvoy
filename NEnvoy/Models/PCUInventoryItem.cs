using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record PCUInventoryItem(
    [property: JsonPropertyName("devices")] IEnumerable<PCUDevice> Devices
) : InventoryItem();
