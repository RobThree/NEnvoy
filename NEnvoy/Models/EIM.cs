using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record EIM
(
    int ActiveCount,
    DateTimeOffset ReadingTime,
    [property: JsonPropertyName("measurementType")] string MeasurementType,
    [property: JsonPropertyName("wNow")] decimal WattsNow,
    [property: JsonPropertyName("whLifetime")] decimal WattHoursNow,
    [property: JsonPropertyName("varhLeadLifetime")] decimal CapacitiveReactantEnergyLifetime,
    [property: JsonPropertyName("varhLagLifetime")] decimal InductiveReactantEnergyLifetime,
    [property: JsonPropertyName("vahLifetime")] decimal ApparentEnergyVoltAmpereHourLifetime,
    [property: JsonPropertyName("rmsCurrent")] decimal RMSCurrent,
    [property: JsonPropertyName("rmsVoltage")] decimal RMSVoltage,
    [property: JsonPropertyName("reactPwr")] decimal ReactivePower,
    [property: JsonPropertyName("apprntPwr")] decimal ApparentPower,
    [property: JsonPropertyName("pwrFactor")] decimal PowerFactor,
    [property: JsonPropertyName("whToday")] decimal WattHoursToday,
    [property: JsonPropertyName("whLastSevenDays")] decimal WattHoursLastSevenDays,
    [property: JsonPropertyName("vahToday")] decimal ApparentEnergyToday,
    [property: JsonPropertyName("varhLeadToday")] decimal CapacitiveReactantEnergyToday,
    [property: JsonPropertyName("varhLagToday")] decimal InductiveReactantEnergyToday
) : ProductionRecord(ActiveCount, ReadingTime);
