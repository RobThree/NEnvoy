using System.Text.Json;
using System.Text.Json.Serialization;

namespace NEnvoy.Internals.Converters;

internal class StringTimestampDateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (value != null && int.TryParse(value, out var parsedvalue))
        {
            return DateTimeOffset.FromUnixTimeSeconds(parsedvalue);
        }

        throw new FormatException();   // TODO: Decent exception
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToUnixTimeSeconds().ToString());
}