using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record RootMeterReading(
    long EId,                           // TODO: int? long? other?
    DateTimeOffset Time,
    decimal ActiveEnergyDelivered,
    decimal ActiveEnergyReceived,
    decimal ApparentEnergy,
    decimal ReactiveEnergyLagging,
    decimal ReactiveEnergyLeading,
    decimal InstantaneousDemand,
    decimal ActivePower,
    decimal ApparentPower,
    decimal ReactivePower,
    decimal PowerFactor,
    decimal Voltage,
    decimal Current,
    decimal Frequency,
    [property: JsonPropertyName("channels")] IEnumerable<MeterReading> Channels
) : MeterReading(
    EId,
    Time,
    ActiveEnergyDelivered,
    ActiveEnergyReceived,
    ApparentEnergy,
    ReactiveEnergyLagging,
    ReactiveEnergyLeading,
    InstantaneousDemand,
    ActivePower,
    ApparentPower,
    ReactivePower,
    PowerFactor,
    Voltage,
    Current,
    Frequency
);