using NEnvoy.Exceptions;
using NEnvoy.Internals;
using NEnvoy.Internals.Models;
using NEnvoy.Models;
using Refit;
using System.Net.Http.Headers;

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
            var token = await entrezclient.RequestTokenAsync(new EnphaseTokenRequest(loginresult.SessionId, envoyInfo.Device.Serial, connectionInfo.Username), cancellationToken).ConfigureAwait(false);

            var session = EnvoySession.Create(baseAddress, connectionInfo.SessionTimeout, token);
            var envoyjsonclient = GetEnvoyJsonClient(baseAddress, session);

            return new EnvoyClient(envoyxmlclient, envoyjsonclient, session);
        }
        throw new LoginFailedException(loginresult.Message);
    }

    private static Uri CreateBaseUri(string host)
        => new($"https://{host}");

    private static IEnvoyJsonApi GetEnvoyJsonClient(Uri baseAddress, EnvoySession session)
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
            }
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}