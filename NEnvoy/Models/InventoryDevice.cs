using NEnvoy.Internals.Converters;
using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public abstract record InventoryDevice
(
    [property: JsonPropertyName("part_num")] string PartNumber,
    [property: JsonPropertyName("installed")][property: JsonConverter(typeof(StringTimestampDateTimeOffsetJsonConverter))] DateTimeOffset Installed,
    [property: JsonPropertyName("serial_num")] string SerialNumber,
    [property: JsonPropertyName("device_status")] IEnumerable<string> DeviceStatus,
    [property: JsonPropertyName("last_rpt_date")][property: JsonConverter(typeof(StringTimestampDateTimeOffsetJsonConverter))] DateTimeOffset LastReportedDate,
    [property: JsonPropertyName("admin_state")] int AdminState, // TODO: Enum?
    [property: JsonPropertyName("dev_type")] int DeviceType, // TODO: Enum?
    [property: JsonPropertyName("created_date")][property: JsonConverter(typeof(StringTimestampDateTimeOffsetJsonConverter))] DateTimeOffset CreatedDate,
    [property: JsonPropertyName("img_load_date")][property: JsonConverter(typeof(StringTimestampDateTimeOffsetJsonConverter))] DateTimeOffset ImageLoadDate,
    [property: JsonPropertyName("img_pnum_running")] string ImagePartnumberRunning,
    [property: JsonPropertyName("ptpn")] string Ptpn,   // TODO: What is this?
    [property: JsonPropertyName("chaneid")] int ChanEID,// TODO: What is this?
    [property: JsonPropertyName("device_control")] IEnumerable<DeviceControl> DeviceControl,
    [property: JsonPropertyName("producing")] bool Producing,
    [property: JsonPropertyName("communicating")] bool Communicating,
    [property: JsonPropertyName("provisioned")] bool Provisioned,
    [property: JsonPropertyName("operating")] bool Operating
);
