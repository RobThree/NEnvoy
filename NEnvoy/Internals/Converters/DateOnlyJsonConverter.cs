using System.Text.Json;
using System.Text.Json.Serialization;

namespace NEnvoy.Internals.Converters;

internal class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (DateOnly.TryParseExact(reader.GetString(), "MM/dd/yyyy", out var result))
        {
            return result;
        }

        throw new FormatException();   // TODO: Decent exception
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString("MM/dd/yyyy"));
}