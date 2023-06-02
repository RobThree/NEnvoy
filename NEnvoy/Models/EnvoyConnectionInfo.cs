namespace NEnvoy.Models;

public record EnvoyConnectionInfo
{
    public const string DefaultHost = "envoy";
    public static readonly TimeSpan DefaultSessionTimeout = TimeSpan.FromMinutes(5);

    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string EnvoyHost { get; init; } = DefaultHost;
    public string EnphaseBaseUri = EnvoyClient.DefaultEnphaseBaseUri;
    public string EnphaseEntrezBaseUri = EnvoyClient.DefaultEntrezBaseUri;
    public TimeSpan SessionTimeout { get; init; } = DefaultSessionTimeout;
}