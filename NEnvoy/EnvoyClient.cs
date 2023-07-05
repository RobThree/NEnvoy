using NEnvoy.Exceptions;
using NEnvoy.Internals;
using NEnvoy.Internals.Models;
using NEnvoy.Models;
using Refit;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NEnvoy;

public class EnvoyClient : IEnvoyClient
{
    public const string DefaultEnphaseBaseUri = "https://enlighten.enphaseenergy.com";
    public const string DefaultEntrezBaseUri = "https://entrez.enphaseenergy.com";

    private readonly IEnvoyXmlApi _envoyxmlclient;
    private readonly IEnvoyJsonApi _envoyjsonclient;
    private readonly EnvoySession _session;

    public string GetToken() => (_session ?? throw new NoActiveSessionException()).Token;

    private EnvoyClient(IEnvoyXmlApi envoyXmlClient, IEnvoyJsonApi envoyJsonClient, EnvoySession session)
    {
        _envoyxmlclient = envoyXmlClient ?? throw new ArgumentNullException(nameof(envoyXmlClient));
        _envoyjsonclient = envoyJsonClient ?? throw new ArgumentNullException(nameof(envoyJsonClient));
        _session = session ?? throw new ArgumentNullException(nameof(session));
    }

    public static EnvoyClient FromToken(string token, EnvoyConnectionInfo connectionInfo)
    {
        var baseuri = CreateBaseUri(connectionInfo.EnvoyHost);
        var session = EnvoySession.Create(baseuri, connectionInfo.SessionTimeout, token);
        var envoyjsonclient = GetEnvoyJsonClient(baseuri, session);
        return new(GetEnvoyXmlClient(baseuri), envoyjsonclient, session);
    }

    public static async Task<EnvoyClient> FromLoginAsync(EnvoyConnectionInfo connectionInfo, CancellationToken cancellationToken = default)
    {
        var baseAddress = CreateBaseUri(connectionInfo.EnvoyHost);
        var envoyxmlclient = GetEnvoyXmlClient(baseAddress);
        var envoyInfo = await GetEnvoyInfoAsync(envoyxmlclient, cancellationToken).ConfigureAwait(false);

        var enphaseclient = RestService.For<IEnphase>(connectionInfo.EnphaseBaseUri);
        var loginresult = await enphaseclient.LoginAsync(new EnphaseLoginRequest(connectionInfo.Username, connectionInfo.Password), cancellationToken).ConfigureAwait(false);
        if (string.Equals(loginresult.Message, "success", StringComparison.OrdinalIgnoreCase))
        {
            var entrezclient = RestService.For<IEntrezEnphase>(connectionInfo.EnphaseEntrezBaseUri);
            var token = await entrezclient.RequestTokenAsync(new EntrezEnphaseTokenRequest(loginresult.SessionId, envoyInfo.Device.Serial, connectionInfo.Username), cancellationToken).ConfigureAwait(false);

            var session = EnvoySession.Create(baseAddress, connectionInfo.SessionTimeout, token);
            var envoyjsonclient = GetEnvoyJsonClient(baseAddress, session);

            return new EnvoyClient(envoyxmlclient, envoyjsonclient, session);
        }
        throw new LoginFailedException(loginresult.Message);
    }

    public static async Task<EnvoyClient> FromUILoginAsync(EnvoyConnectionInfo connectionInfo, string deviceSerial, CancellationToken cancellationToken = default)
    {
        var baseAddress = CreateBaseUri(connectionInfo.EnvoyHost);
        string GenerateRandomString(int length) {
            var chars = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
            return string.Join(
                string.Empty, 
                Enumerable.Range(0,length).Select(c => chars[Random.Shared.Next(chars.Length)])
            );
        }
        string GenerateChallenge(string value)
            => Convert.ToBase64String(
                SHA256.HashData(Encoding.UTF8.GetBytes(value))
            )
            .Replace("+", "-")
            .Replace("/", "_")
            .Replace("=", string.Empty);
        
        var secret = GenerateRandomString(43);
        var entrezclient = GetEntrezEnphaseJsonClient(new Uri(connectionInfo.EnphaseEntrezBaseUri));

        var callbackuri = $"{CreateBaseUri(connectionInfo.EnvoyHost)}auth/callback";

        var result = await entrezclient.LoginAsync(new EntrezEnphaseLoginRequest(
            connectionInfo.Username,
            connectionInfo.Password,
            GenerateChallenge(secret),
            callbackuri,
            "envoy-ui",
            "envoy-ui-client",
            "oauth",
            deviceSerial,
            "authorize",
            string.Empty,
            string.Empty
        ), cancellationToken).ConfigureAwait(false);

        var querystring = HttpUtility.ParseQueryString(result.Headers.Location?.Query ?? string.Empty);
        var code = querystring.Get("code");
        
        if (string.IsNullOrEmpty(code))
            throw new Exception("Unable to parse code from return URI");  //TODO: Decent exception

        var client = GetEnvoyJsonClient(baseAddress);
        var jwtresult = await client.GetJWT(
            new JWTRequest(
                "envoy-ui-1",
                "authorization_code",
                callbackuri,
                secret,
                code
            ),
            cancellationToken
        ).ConfigureAwait(false);

        var session = EnvoySession.Create(baseAddress, connectionInfo.SessionTimeout, jwtresult.AccessToken);
        session.UpdateLastRequest();
        var envoyjsonclient = GetEnvoyJsonClient(baseAddress, session);
        return new EnvoyClient(
            GetEnvoyXmlClient(baseAddress),
            envoyjsonclient, 
            session
        );
    }

    private static Uri CreateBaseUri(string host)
        => new($"https://{host}");

    private static IEntrezEnphase GetEntrezEnphaseJsonClient(Uri baseAddress)
        => RestService.For<IEntrezEnphase>(GetSafeClient(baseAddress));

    private static IEnvoyJsonApi GetEnvoyJsonClient(Uri baseAddress, EnvoySession? session = null)
        => RestService.For<IEnvoyJsonApi>(GetUnsafeClient(baseAddress, session));

    private static IEnvoyXmlApi GetEnvoyXmlClient(Uri baseAddress)
        => RestService.For<IEnvoyXmlApi>(GetUnsafeClient(baseAddress), new RefitSettings
        {
            ContentSerializer = new XmlContentSerializer()
        });
    private static Task<EnvoyInfo> GetEnvoyInfoAsync(IEnvoyXmlApi envoyClient, CancellationToken cancellationToken = default)
        => envoyClient.GetEnvoyInfoAsync(cancellationToken);

    public Task<EnvoyInfo> GetEnvoyInfoAsync(CancellationToken cancellationToken = default)
        => _envoyxmlclient.GetEnvoyInfoAsync(cancellationToken);

    public Task<IEnumerable<ConsumptionReport>> GetConsumptionAsync(CancellationToken cancellationToken = default)
        => _envoyjsonclient.GetConsumptionAsync(cancellationToken);

    public Task<IEnumerable<V1Inverter>> GetV1InvertersAsync(CancellationToken cancellationToken = default)
        => _envoyjsonclient.GetV1InvertersAsync(cancellationToken);

    public Task<V1Production> GetV1ProductionAsync(CancellationToken cancellationToken = default)
        => _envoyjsonclient.GetV1ProductionAsync(cancellationToken);

    public Task<IEnumerable<Meter>> GetMetersAsync(CancellationToken cancellationToken = default)
        => _envoyjsonclient.GetMetersAsync(cancellationToken);

    public Task<IEnumerable<RootMeterReading>> GetMeterReadingsAsync(CancellationToken cancellationToken = default)
        => _envoyjsonclient.GetMeterReadingsAsync(cancellationToken);

    public async Task<DeviceStatus> GetDeviceStatusAsync(IEqualityComparer<string>? equalityComparer = null, CancellationToken cancellationToken = default)
    {
        // Try to make the Envoy mess a little more bearable
        var result = await _envoyjsonclient.GetDeviceStatusAsync(cancellationToken).ConfigureAwait(false);
        return new DeviceStatus(
            new DeviceStatusCounters(
                result.Counters.TryGetValue("pcu", out var pcu) ? pcu.ToDeviceStatusCounter() : null,
                result.Counters.TryGetValue("acb", out var acb) ? acb.ToDeviceStatusCounter() : null,
                result.Counters.TryGetValue("nsrb", out var nsrb) ? nsrb.ToDeviceStatusCounter() : null,
                result.Counters.TryGetValue("pld", out var pld) ? pld.ToDeviceStatusCounter() : null,
                result.Counters.TryGetValue("esub", out var esub) ? esub.ToDeviceStatusCounter() : null
            ),
            result.PCU?.ToDeviceStatusValues(equalityComparer),
            result.ACB?.ToDeviceStatusValues(equalityComparer),
            result.NSRB?.ToDeviceStatusValues(equalityComparer),
            result.PLD?.ToDeviceStatusValues(equalityComparer),
            result.ESUB?.ToDeviceStatusValues(equalityComparer)
        );
    }

    public Task<WirelessDisplay> GetWirelessDisplayAsync(CancellationToken cancellationToken = default)
        => _envoyjsonclient.GetWirelessDisplayAsync(cancellationToken);

    public Task<WirelessDisplayExtended> GetWirelessDisplayExtendedAsync(CancellationToken cancellationToken = default)
        => _envoyjsonclient.GetWirelessDisplayExtendedAsync(cancellationToken);

    public Task<ProductionData> GetProductionAsync(CancellationToken cancellationToken = default)
        => _envoyjsonclient.GetProductionAsync(cancellationToken);

    public Task<IEnumerable<InventoryItem>> GetInventoryAsync(CancellationToken cancellationToken = default)
        => _envoyjsonclient.GetInventoryAsync(cancellationToken);

    public Task<Home> GetHomeAsync(CancellationToken cancellationToken = default)
        => _envoyjsonclient.GetHomeAsync(cancellationToken);

    // Returns an HttpClient that ignores SSL errors since Envoy uses self-signed certificates
    private static HttpClient GetUnsafeClient(Uri baseAddress, EnvoySession? session = null)
    {
        var handler = new EnvoyHttpClientHandler(session)
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, sslErrors) => true,
        };
        if (session?.CookieContainer != null)
        {
            handler.CookieContainer = session.CookieContainer;
            handler.UseCookies = true;
        }
        var client = new HttpClient(handler)
        {
            BaseAddress = baseAddress
        };
        return client;
    }

    private static HttpClient GetSafeClient(Uri baseAddress)
        => new HttpClient(new EnvoyHttpClientHandler(null) {
            AllowAutoRedirect = false
        })
        {
            BaseAddress = baseAddress
        };

    private class EnvoyHttpClientHandler : HttpClientHandler
    {
        private readonly EnvoySession? _session;

        public EnvoyHttpClientHandler(EnvoySession? session)
            => _session = session;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_session != null)
            {
                if (_session.Expired)
                {
                    var authrequest = new HttpRequestMessage(HttpMethod.Post, new Uri(_session.BaseAddress, "/auth/check_jwt"));
                    authrequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _session.Token);
                    (await base.SendAsync(authrequest, cancellationToken).ConfigureAwait(false)).EnsureSuccessStatusCode();
                }
                _session.UpdateLastRequest();

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _session.Token);
            }
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}