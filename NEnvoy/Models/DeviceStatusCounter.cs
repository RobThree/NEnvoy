namespace NEnvoy.Models;

public record DeviceStatusCounter
(
    int Expected,
    int Discovered,
    int CtrlsTotal,  // TODO: Ctrls is ... controllers??
    int CtrlsGone,
    int CtrlsCommunicating,
    int ChannelsTotal,
    int ChannelsRecent,
    int ChannelsProducing
);