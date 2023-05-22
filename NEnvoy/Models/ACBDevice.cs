namespace NEnvoy.Models;

// TODO: Below properties are assumed and may differ
public record ACBDevice
(
    string PartNumber,
    DateTimeOffset Installed,
    string SerialNumber,
    IEnumerable<string> DeviceStatus,
    DateTimeOffset LastReportedDate,
    int AdminState, // TODO: Enum?
    int DeviceType, // TODO: Enum?
    DateTimeOffset CreatedDate,
    DateTimeOffset ImageLoadDate,
    string ImagePartnumberRunning,
    string Ptpn,   // TODO: What is this?
    int ChanEID,// TODO: What is this?
    IEnumerable<DeviceControl> DeviceControl,
    bool Producing,
    bool Communicating,
    bool Provisioned,
    bool Operating
) : InventoryDevice(PartNumber, Installed, SerialNumber, DeviceStatus, LastReportedDate, AdminState, DeviceType, CreatedDate, ImageLoadDate, ImagePartnumberRunning, Ptpn, ChanEID, DeviceControl, Producing, Communicating, Provisioned, Operating);
