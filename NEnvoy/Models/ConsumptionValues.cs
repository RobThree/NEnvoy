using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record ConsumptionValues(
    [property: JsonPropertyName("currW")] decimal CurrentWatts,
    [property: JsonPropertyName("actPower")] decimal ActualPower,
    [property: JsonPropertyName("apprntPwr")] decimal ApparentPower,
    [property: JsonPropertyName("reactPwr")] decimal ReactivePower,
    [property: JsonPropertyName("whDlvdCum")] decimal WattHoursDelivered,
    [property: JsonPropertyName("whRcvdCum")] decimal WattHoursReceived,
    [property: JsonPropertyName("varhLagCum")] decimal InductiveReactantEnergy,
    [property: JsonPropertyName("varhLeadCum")] decimal CapacitiveReactantEnergy,
    [property: JsonPropertyName("vahCum")] decimal ApparentEnergyVoltAmpereHour,
    [property: JsonPropertyName("rmsVoltage")] decimal RMSVoltage,
    [property: JsonPropertyName("rmsCurrent")] decimal RMSCurrent,
    [property: JsonPropertyName("pwrFactor")] decimal PowerFactor,
    [property: JsonPropertyName("freqHz")] decimal FrequencyHerz
);