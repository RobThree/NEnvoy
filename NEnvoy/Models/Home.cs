using NEnvoy.Internals.Converters;
using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record Home
(
    [property: JsonPropertyName("software_build_epoch")][property: JsonConverter(typeof(IntTimestampDateTimeOffsetJsonConverter))] DateTimeOffset SoftwareBuildEpoch,
    [property: JsonPropertyName("is_nonvoy")] bool IsNonVoy,
    [property: JsonPropertyName("db_size")] int DbSize,    //TODO: Maybe long?
    [property: JsonPropertyName("db_percent_full")][property: JsonConverter(typeof(StringDecimalJsonConverter))] decimal? DbPercentFull,
    [property: JsonPropertyName("timezone")] string TimeZone,
    [property: JsonPropertyName("current_date")][property: JsonConverter(typeof(DateOnlyJsonConverter))] DateOnly CurrentDate,
    [property: JsonPropertyName("current_time")][property: JsonConverter(typeof(TimeOnlyJsonConverter))] TimeOnly CurrentTime,
    [property: JsonPropertyName("network")] Network Network,
    [property: JsonPropertyName("tariff")] string Tariff,       // TODO: Enum?
    /*TODO: what is this?
    {
      "comm": {
        "num": 29,
        "level": 4,
        "pcu": {
          "num": 28,
          "level": 5
        },
        "acb": {
          "num": 0,
          "level": 0
        },
        "nsrb": {
          "num": 1,
          "level": 5
        },
        "esub": {
          "num": 0,
          "level": 0
        },
        "encharge": [
          {
            "num": 0,
            "level": 0,
            "level_24g": 0,
            "level_subg": 0
          }
        ]
      },
    }
    */

    [property: JsonPropertyName("alerts")] IEnumerable<Alert> Alerts,
    [property: JsonPropertyName("update_status")] string UpdateStatus,
    [property: JsonPropertyName("wireless_connection")] IEnumerable<WirelessConnectionStatus> WirelessConnectionStatus,
    [property: JsonPropertyName("enpower")] EnpowerStatus Enpower
);