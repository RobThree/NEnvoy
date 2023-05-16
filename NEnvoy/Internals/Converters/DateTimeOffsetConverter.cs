using System.Text.Json;
using System.Text.Json.Serialization;

namespace NEnvoy.Internals.Converters;

internal class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TryGetInt32(out var value))
        {
            return DateTimeOffset.FromUnixTimeSeconds(value);
        }

        throw new InvalidDataException();   // TODO: Decent exception
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        => writer.WriteNumberValue(value.ToUnixTimeSeconds());
}