using NEnvoy.Internals.Converters;
using System.Net;
using System.Text.Json.Serialization;

namespace NEnvoy.Models;

//TODO: We currently can't use the below commented-out code because Enphase doesn't return the "type" property as first value in the json object
//      See https://github.com/dotnet/runtime/issues/72604#issuecomment-1557131256

//[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
//[JsonDerivedType(typeof(EthernetInterface), typeDiscriminator: "ethernet")]
//[JsonDerivedType(typeof(WiFiInterface), typeDiscriminator: "wifi")]
//public abstract record NetworkInterface
//(
//    [property: JsonPropertyName("interface")] string Interface,
//    [property: JsonPropertyName("mac")] string MacAddress,
//    [property: JsonPropertyName("dhcp")] bool DHCP,
//    [property: JsonPropertyName("ip")][property: JsonConverter(typeof(IPAddressJsonConverter))] IPAddress? IP,
//    [property: JsonPropertyName("carrier")] bool Carrier,
//    [property: JsonPropertyName("signal_strength")] decimal SignalStrength,         // TODO: Is decimal the correct type?
//    [property: JsonPropertyName("signal_strength_max")] decimal SignalStrengthMax   // TODO: Is decimal the correct type?
//);

//public record EthernetInterface
//(
//    string Interface,
//    string MacAddress,
//    bool DHCP,
//    IPAddress? IP,
//    bool Carrier,
//    decimal SignalStrength,
//    decimal SignalStrengthMax
//) : NetworkInterface(Interface, MacAddress, DHCP, IP, Carrier, SignalStrength, SignalStrengthMax);

//public record WiFiInterface
//(
//    string Interface,
//    string MacAddress,
//    bool DHCP,
//    IPAddress? IP,
//    bool Carrier,
//    decimal SignalStrength,
//    decimal SignalStrengthMax
//    [property: JsonPropertyName("supported")] bool Supported,
//    [property: JsonPropertyName("present")] bool Present,
//    [property: JsonPropertyName("configured")] bool Configured,
//    [property: JsonPropertyName("status")] string Status   //TODO: Enum?
//) : NetworkInterface(Interface, MacAddress, DHCP, IP, Carrier, SignalStrength, SignalStrengthMax);

// So for now we use a single object with a Type property (and some nullable properties)... which is ugly but it'll have to do for now
public record NetworkInterface
(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("interface")] string Interface,
    [property: JsonPropertyName("mac")] string MacAddress,
    [property: JsonPropertyName("dhcp")] bool DHCP,
    [property: JsonPropertyName("ip")][property: JsonConverter(typeof(IPAddressJsonConverter))] IPAddress? IP,
    [property: JsonPropertyName("carrier")] bool Carrier,
    [property: JsonPropertyName("signal_strength")] decimal SignalStrength,         // TODO: Is decimal the correct type?
    [property: JsonPropertyName("signal_strength_max")] decimal SignalStrengthMax,   // TODO: Is decimal the correct type?
    [property: JsonPropertyName("supported")] bool? Supported,
    [property: JsonPropertyName("present")] bool? Present,
    [property: JsonPropertyName("configured")] bool? Configured,
    [property: JsonPropertyName("status")] string? Status   //TODO: Enum?
);
