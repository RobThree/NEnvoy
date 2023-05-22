using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record NSRBDevice
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
    bool Operating,
    [property: JsonPropertyName("relay")] string Relay, //TODO: Enum?
    [property: JsonPropertyName("reason_code")] int ReasonCode,
    [property: JsonPropertyName("reason")] string Reason,   //TODO: Enum?
    [property: JsonPropertyName("ine-count")] int LineCount,
    [property: JsonPropertyName("line1-connected")] bool Line1Connected,
    [property: JsonPropertyName("line2-connected")] bool Line2Connected,
    [property: JsonPropertyName("line3-connected")] bool Line3Connected
) : InventoryDevice(PartNumber, Installed, SerialNumber, DeviceStatus, LastReportedDate, AdminState, DeviceType, CreatedDate, ImageLoadDate, ImagePartnumberRunning, Ptpn, ChanEID, DeviceControl, Producing, Communicating, Provisioned, Operating);
