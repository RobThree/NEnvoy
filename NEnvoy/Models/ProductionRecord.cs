using NEnvoy.Internals.Converters;
using System.Text.Json.Serialization;

namespace NEnvoy.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(Inverters), typeDiscriminator: "inverters")]
[JsonDerivedType(typeof(EIM), typeDiscriminator: "eim")]
[JsonDerivedType(typeof(Battery), typeDiscriminator: "acb")]
public abstract record ProductionRecord
(
    [property: JsonPropertyName("activeCount")] int ActiveCount,
    [property: JsonPropertyName("readingTime")][property: JsonConverter(typeof(IntTimestampDateTimeOffsetJsonConverter))] DateTimeOffset ReadingTime
);
