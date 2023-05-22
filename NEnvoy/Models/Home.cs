using NEnvoy.Internals.Converters;
using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record Home
(
    [property: JsonPropertyName("software_build_epoch")][property: JsonConverter(typeof(TimestampDateTimeOffsetJsonConverter))] DateTimeOffset SoftwareBuildEpoch,
    [property: JsonPropertyName("is_nonvoy")] bool IsNonVoy,
    [property: JsonPropertyName("db_size")] int DbSize,    //TODO: Maybe long?
    [property: JsonPropertyName("db_percent_full")][property: JsonConverter(typeof(StringDecimalJsonConverter))] decimal? DbPercentFull,
    [property: JsonPropertyName("timezone")] string TimeZone,
    [property: JsonPropertyName("current_date")][property: JsonConverter(typeof(DateOnlyJsonConverter))] DateOnly CurrentDate,
    [property: JsonPropertyName("current_time")][property: JsonConverter(typeof(TimeOnlyJsonConverter))] TimeOnly CurrentTime

// TODO: Rest...
);