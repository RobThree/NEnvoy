using NEnvoy.Models;

namespace TestApp;

public class Configuration
{
    public ConnectionInfo Envoy { get; init; } = new();
    public Session? Session { get; init; }
}
