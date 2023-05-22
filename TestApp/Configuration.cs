using NEnvoy.Models;

namespace TestApp;

public class Configuration
{
    public EnvoyConnectionInfo Envoy { get; init; } = new();
}
