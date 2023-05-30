using System.Text.Json.Serialization;

namespace NEnvoy.Internals.Models;
internal record IVPDeviceStatus
(
    [property: JsonPropertyName("counters")] Dictionary<string, IVPDeviceStatusCounterValues> Counters,
    [property: JsonPropertyName("pcu")] IVPDeviceStatusValues? PCU,
    [property: JsonPropertyName("acb")] IVPDeviceStatusValues? ACB,
    [property: JsonPropertyName("nsrb")] IVPDeviceStatusValues? NSRB,
    [property: JsonPropertyName("pld")] IVPDeviceStatusValues? PLD,
    [property: JsonPropertyName("esub")] IVPDeviceStatusValues? ESUB
);
