using System.Text.Json.Serialization;

namespace NEnvoy.Models;

//TODO: We currently can't use the below commented-out code because Enphase doesn't return the "type" property as first value in the json object
//      See https://github.com/dotnet/runtime/issues/72604#issuecomment-1557131256

//[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
//[JsonDerivedType(typeof(ZigbeeConnectionStatus), typeDiscriminator: "zigbee")]
//[JsonDerivedType(typeof(SubGhzConnectionStatus), typeDiscriminator: "subghz")]
//public abstract record WirelessConnectionStatus
//(
//    [property: JsonPropertyName("signal_strength")] decimal SignalStrength,         // TODO: Is decimal the correct type?
//    [property: JsonPropertyName("signal_strength_max")] decimal SignalStrengthMax,  // TODO: Is decimal the correct type?
//    [property: JsonPropertyName("connected")] bool Connected
//);

//public record ZigbeeConnectionStatus
//(
//    decimal SignalStrength,
//    decimal SignalStrengthMax,
//    bool Connected
//) : WirelessConnectionStatus(SignalStrength, SignalStrengthMax, Connected);

//public record SubGhzConnectionStatus
//(
//    decimal SignalStrength,
//    decimal SignalStrengthMax,
//    bool Connected
//) : WirelessConnectionStatus(SignalStrength, SignalStrengthMax, Connected);

public record WirelessConnectionStatus
(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("signal_strength")] decimal SignalStrength,         // TODO: Is decimal the correct type?
    [property: JsonPropertyName("signal_strength_max")] decimal SignalStrengthMax,  // TODO: Is decimal the correct type?
    [property: JsonPropertyName("connected")] bool Connected
);