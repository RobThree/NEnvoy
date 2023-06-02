using System.Diagnostics.CodeAnalysis;
using System.Net;

internal record EnvoySession(
    Uri BaseAddress,
    string Token,
    CookieContainer CookieContainer
)
{
    private const string _cookie_sessionid = "sessionId";
    public string Id => TryGetCookieValue(_cookie_sessionid, out var value) ? value : string.Empty;

    internal static EnvoySession Create(Uri baseAddress, string token)
    {
        var c = new CookieContainer();
        return new EnvoySession(baseAddress, token, c);
    }

    private bool TryGetCookie(string name, [NotNullWhen(true)] out Cookie? cookie)
    {
        var cookies = CookieContainer.GetCookies(BaseAddress).Cast<Cookie>();
        cookie = cookies.FirstOrDefault(c => c.Name == name);
        return cookie != null;
    }

    private bool TryGetCookieValue(string name, [NotNullWhen(true)] out string? value)
    {
        value = null;
        if (TryGetCookie(name, out var cookie))
        {
            value = cookie.Value;
            return true;
        }
        return false;
    }
}