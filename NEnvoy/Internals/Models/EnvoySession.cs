using System.Diagnostics.CodeAnalysis;
using System.Net;

internal record EnvoySession(
    Uri BaseAddress,
    string Token,
    bool IsConsumer,
    CookieContainer CookieContainer
) {
    private const string cookie_sessionid = "sessionId";
    public string Id => TryGetCookieValue(cookie_sessionid, out var value) ? value : string.Empty;

    internal static EnvoySession Create(Uri baseAddress, SessionInfo sessionInfo) {
        var c = new CookieContainer();
        c.Add(baseAddress, new Cookie(cookie_sessionid, sessionInfo.Id));
        return new EnvoySession(baseAddress, sessionInfo.Token, sessionInfo.IsConsumer, c);
    }

    private bool TryGetCookie(string name, [NotNullWhen(true)] out Cookie? cookie) {
        var cookies = CookieContainer.GetCookies(BaseAddress).Cast<Cookie>();
        cookie = cookies.FirstOrDefault(c => c.Name == name);
        return cookie != null;
    }

    private bool TryGetCookieValue(string name, [NotNullWhen(true)] out string? value)
    {
        value = null;
        if (TryGetCookie(name, out var cookie)) {
            value = cookie.Value;
            return true;
        }
        return false;
    } 
}