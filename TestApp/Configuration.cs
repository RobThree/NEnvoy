using NEnvoy.Models;

namespace TestApp;

public class Configuration
{
    public EnvoyConnectionInfo Envoy { get; init; } = new();
    public SessionInfo? Session { get; init; } = null;
}