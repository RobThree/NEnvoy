namespace NEnvoy.Models;

public record DeviceStatusCounters
(
    DeviceStatusCounter? PCU,
    DeviceStatusCounter? ACB,
    DeviceStatusCounter? NSRB,
    DeviceStatusCounter? PLD,
    DeviceStatusCounter? ESUB
);
