using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NEnvoy.Internals.Converters;
internal class IPAddressJsonConverter : JsonConverter<IPAddress?>
{
    public override IPAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (value == null)
        {
            return null;
        }

        return IPAddress.TryParse(reader.GetString(), out var ip) ? ip : throw new InvalidDataException();  // TODO: Decent exception
    }


    public override void Write(Utf8JsonWriter writer, IPAddress? value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
        }
        else
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}