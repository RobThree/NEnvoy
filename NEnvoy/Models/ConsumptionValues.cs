using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record ConsumptionValues(
    [property: JsonPropertyName("currW")] decimal CurrentWatts,
    [property: JsonPropertyName("actPower")] decimal ActualPower,
    [property: JsonPropertyName("apprntPwr")] decimal ApparentPower,
    [property: JsonPropertyName("reactPwr")] decimal ReactivePower,
    [property: JsonPropertyName("whDlvdCum")] decimal WattHoursDeliveredCumulative,
    [property: JsonPropertyName("whRcvdCum")] decimal WattHoursReceivedCumulative,
    [property: JsonPropertyName("varhLagCum")] decimal InductiveReactantEnergyCumulative,
    [property: JsonPropertyName("varhLeadCum")] decimal CapacitiveReactantEnergyCumulative,
    [property: JsonPropertyName("vahCum")] decimal ApparentEnergyVoltAmpereHourCumulative,
    [property: JsonPropertyName("rmsVoltage")] decimal RMSVoltage,
    [property: JsonPropertyName("rmsCurrent")] decimal RMSCurrent,
    [property: JsonPropertyName("pwrFactor")] decimal PowerFactor,
    [property: JsonPropertyName("freqHz")] decimal FrequencyHerz
);