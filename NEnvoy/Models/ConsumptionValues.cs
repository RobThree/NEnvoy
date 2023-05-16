using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record ConsumptionValues(
    [property: JsonPropertyName("currW")] decimal CurrentWatts,
    [property: JsonPropertyName("actPower")] decimal ActualPower,
    [property: JsonPropertyName("apprntPwr")] decimal ApparentPower,
    [property: JsonPropertyName("reactPwr")] decimal ReactivePower,
/*TODO: Determine proper properties for the following values:
 * 
        "whDlvdCum": 10021.568,
        "whRcvdCum": 0,
        "varhLagCum": 21418.98,
        "varhLeadCum": 45504.594,
        "vahCum": 129554.175,
*/
    [property: JsonPropertyName("rmsVoltage")] decimal RMSVoltage,
    [property: JsonPropertyName("rmsCurrent")] decimal RMSCurrent,
    [property: JsonPropertyName("pwrFactor")] decimal PowerFactor,
    [property: JsonPropertyName("freqHz")] decimal FrequencyHerz
);
