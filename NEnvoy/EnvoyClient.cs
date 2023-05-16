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

    private readonly IEnvoyXml _envoyxmlclient;
    private readonly IEnvoyJson _envoyjsonclient;
    
    public string SessionToken { get; private set; }
    public bool IsConsumer { get; private set; }

    private EnvoyClient(IEnvoyXml envoyXmlClient, IEnvoyJson envoyJsonClient, string sessionToken, bool isconsumer)
    {
        _envoyxmlclient = envoyXmlClient ?? throw new ArgumentNullException(nameof(envoyXmlClient));
        _envoyjsonclient = envoyJsonClient ?? throw new ArgumentNullException(nameof(envoyJsonClient));
        SessionToken = sessionToken ?? throw new ArgumentNullException(nameof(sessionToken));
        IsConsumer = isconsumer;
    }

    public static EnvoyClient FromSessionToken(string host, string sessionToken, bool isConsumer = true)
        => new(GetEnvoyXmlClient(host, sessionToken), GetEnvoyClient(host, sessionToken), sessionToken, isConsumer);

    public static async Task<EnvoyClient> FromLoginAsync(ConnectionInfo connectionInfo, CancellationToken cancellationToken = default)
    {
        var envoyxmlclient = GetEnvoyXmlClient(connectionInfo.EnvoyHost);
        var envoyInfo = await GetEnvoyInfoAsync(envoyxmlclient, cancellationToken).ConfigureAwait(false);

        var enphaseclient = RestService.For<IEnphase>(connectionInfo.EnphaseBaseUri);
        var loginresult = await enphaseclient.LoginAsync(new EnphaseLoginRequest(connectionInfo.Username, connectionInfo.Password), cancellationToken).ConfigureAwait(false);
        if (string.Equals(loginresult.Message, "success", StringComparison.OrdinalIgnoreCase))
        {
            var entrezclient = RestService.For<IEntrezEnphase>(connectionInfo.EnphaseEntrezBaseUri);
            var token = await entrezclient.RequestTokenAsync(new EnphaseTokenRequest(loginresult.SessionId, envoyInfo.Device.Serial, connectionInfo.Username), cancellationToken).ConfigureAwait(false);
            var envoyjsonclient = GetEnvoyClient(connectionInfo.EnvoyHost, token);
            return new EnvoyClient(envoyxmlclient, envoyjsonclient, token, loginresult.IsConsumer);
        }
        throw new LoginFailedException(loginresult.Message);
    }

    private static IEnvoyJson GetEnvoyClient(string host, string? sessionToken = null)
        => RestService.For<IEnvoyJson>(GetUnsafeClient($"https://{host}", sessionToken));

    private static IEnvoyXml GetEnvoyXmlClient(string host, string? sessionToken = null)
        => RestService.For<IEnvoyXml>(GetUnsafeClient($"https://{host}", sessionToken), new RefitSettings
        {
            ContentSerializer = new XmlContentSerializer()
        });
    private static Task<EnvoyInfo> GetEnvoyInfoAsync(IEnvoyXml envoyClient, CancellationToken cancellationToken = default)
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

    public Task<WirelessDisplay> GetWirelessDisplayAsync(CancellationToken cancellationToken = default)
        => _envoyjsonclient.GetWirelessDisplayAsync(cancellationToken);

    public Task<WirelessDisplayExtended> GetWirelessDisplayExtendedAsync(CancellationToken cancellationToken = default)
        => _envoyjsonclient.GetWirelessDisplayExtendedAsync(cancellationToken);

    public Task<Home> GetHome(CancellationToken cancellationToken = default)
        => _envoyjsonclient.GetHome(cancellationToken);

    // Returns an HttpClient that ignores SSL errors since Envoy uses self-signed certificates
    private static HttpClient GetUnsafeClient(string baseAddress, string? sessionToken)
    {
        var client = new HttpClient(new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, sslErrors) => true,
        })
        {
            BaseAddress = new Uri(baseAddress),
        };
        if (!string.IsNullOrEmpty(sessionToken))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionToken);
        }

        return client;
    }
}