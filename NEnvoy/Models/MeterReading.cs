using NEnvoy.Internals.Converters;
using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record MeterReading(
    [property: JsonPropertyName("eid")] long EId,                           // TODO: int? long? other?
    [property: JsonPropertyName("timestamp")][property: JsonConverter(typeof(IntTimestampDateTimeOffsetJsonConverter))] DateTimeOffset Time,
    [property: JsonPropertyName("actEnergyDlvd")] decimal ActiveEnergyDelivered,
    [property: JsonPropertyName("actEnergyRcvd")] decimal ActiveEnergyReceived,
    [property: JsonPropertyName("apparentEnergy")] decimal ApparentEnergy,
    [property: JsonPropertyName("reactEnergyLagg")] decimal ReactiveEnergyLagging,
    [property: JsonPropertyName("reactEnergyLead")] decimal ReactiveEnergyLeading,
    [property: JsonPropertyName("instantaneousDemand")] decimal InstantaneousDemand,
    [property: JsonPropertyName("activePower")] decimal ActivePower,
    [property: JsonPropertyName("apparentPower")] decimal ApparentPower,
    [property: JsonPropertyName("reactivePower")] decimal ReactivePower,
    [property: JsonPropertyName("pwrFactor")] decimal PowerFactor,
    [property: JsonPropertyName("voltage")] decimal Voltage,
    [property: JsonPropertyName("current")] decimal Current,
    [property: JsonPropertyName("freq")] decimal Frequency
);