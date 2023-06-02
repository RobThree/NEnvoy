using System.Net;

internal record EnvoySession(
    Uri BaseAddress,
    string Token,
    TimeSpan SessionTimeout,
    CookieContainer CookieContainer
)
{
    private DateTimeOffset _lastrequest = DateTimeOffset.MinValue;

    public bool Expired => DateTimeOffset.UtcNow - _lastrequest > SessionTimeout;

    public static EnvoySession Create(Uri baseAddress, TimeSpan sessionTimeout, string token)
    {
        var c = new CookieContainer();
        return new EnvoySession(baseAddress, token, sessionTimeout, c);
    }

    public void UpdateLastRequest() => _lastrequest = DateTimeOffset.UtcNow;
}