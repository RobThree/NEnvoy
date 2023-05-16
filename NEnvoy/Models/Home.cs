using NEnvoy.Internals.Converters;
using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record Home
(
    [property: JsonPropertyName("software_build_epoch")][property: JsonConverter(typeof(DateTimeOffsetJsonConverter))] DateTimeOffset SoftwareBuildEpoch,
    [property: JsonPropertyName("is_nonvoy")] bool IsNonVoy,
    [property: JsonPropertyName("db_size")] int DbSize,    //TODO: Maybe long?
    [property: JsonPropertyName("db_percent_full")][property: JsonConverter(typeof(StringDecimalJsonConverter))] decimal? DbPercentFull,
    [property: JsonPropertyName("timezone")] string TimeZone,
    [property: JsonPropertyName("current_date")] string CurrentDate,      // TODO: Should be DateOnly (but no netstandard support 😣) or collapse both properties into one DateTimeOffset
    [property: JsonPropertyName("current_time")] string CurrentTime       // TODO: Should be TimeOnly (but no netstandard support 😣) or collapse both properties into one DateTimeOffset
                                                                          // TODO: Rest...
);