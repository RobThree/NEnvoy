using System.Text.Json;
using System.Text.Json.Serialization;

namespace NEnvoy.Internals.Converters;

internal class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (TimeOnly.TryParseExact(reader.GetString(), "HH:mm", out var result))
        {
            return result;
        }

        throw new FormatException();   // TODO: Decent exception
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString("HH:mm"));
}