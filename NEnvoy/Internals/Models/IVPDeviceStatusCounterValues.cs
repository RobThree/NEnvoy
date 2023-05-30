using System.Text.Json.Serialization;

namespace NEnvoy.Internals.Models;

internal record IVPDeviceStatusCounterValues
(
    [property: JsonPropertyName("expected")] int Expected,
    [property: JsonPropertyName("discovered")] int Discovered,
    [property: JsonPropertyName("ctrlsTotal")] int CtrlsTotal,  // TODO: Ctrls is ... controllers??
    [property: JsonPropertyName("ctrlsGone")] int CtrlsGone,
    [property: JsonPropertyName("ctrlsCommunicating")] int CtrlsCommunicating,
    [property: JsonPropertyName("chansTotal")] int ChannelsTotal,
    [property: JsonPropertyName("chansRecent")] int ChannelsRecent,
    [property: JsonPropertyName("chansProducing")] int ChannelsProducing
);
