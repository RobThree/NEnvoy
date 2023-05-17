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
    
    public EnvoySession Session { get; private set; }

    private EnvoyClient(IEnvoyXmlApi envoyXmlClient, IEnvoyJsonApi envoyJsonClient, EnvoySession session)
    {
        _envoyxmlclient = envoyXmlClient ?? throw new ArgumentNullException(nameof(envoyXmlClient));
        _envoyjsonclient = envoyJsonClient ?? throw new ArgumentNullException(nameof(envoyJsonClient));
        Session = session ?? throw new ArgumentNullException(nameof(session));
    }

    public static EnvoyClient FromSession(string host, EnvoySession session)
        => new(GetEnvoyXmlClient(host, session), GetEnvoyJsonClient(host, session), session);

    public static async Task<EnvoyClient> FromLoginAsync(EnvoyConnectionInfo connectionInfo, CancellationToken cancellationToken = default)
    {
        var envoyxmlclient = GetEnvoyXmlClient(connectionInfo.EnvoyHost, EnvoySession.NullSession);
        var envoyInfo = await GetEnvoyInfoAsync(envoyxmlclient, cancellationToken).ConfigureAwait(false);

        var enphaseclient = RestService.For<IEnphase>(connectionInfo.EnphaseBaseUri);
        var loginresult = await enphaseclient.LoginAsync(new EnphaseLoginRequest(connectionInfo.Username, connectionInfo.Password), cancellationToken).ConfigureAwait(false);
        if (string.Equals(loginresult.Message, "success", StringComparison.OrdinalIgnoreCase))
        {
            var entrezclient = RestService.For<IEntrezEnphase>(connectionInfo.EnphaseEntrezBaseUri);
            var token = await entrezclient.RequestTokenAsync(new EnphaseTokenRequest(loginresult.SessionId, envoyInfo.Device.Serial, connectionInfo.Username), cancellationToken).ConfigureAwait(false);
            var session = new EnvoySession(token, loginresult.SessionId, loginresult.IsConsumer);
            var envoyjsonclient = GetEnvoyJsonClient(connectionInfo.EnvoyHost, session);
            return new EnvoyClient(envoyxmlclient, envoyjsonclient, session);
        }
        throw new LoginFailedException(loginresult.Message);
    }

    private static IEnvoyJsonApi GetEnvoyJsonClient(string host, EnvoySession session)
        => RestService.For<IEnvoyJsonApi>(GetUnsafeClient($"https://{host}", session));

    private static IEnvoyXmlApi GetEnvoyXmlClient(string host, EnvoySession session)
        => RestService.For<IEnvoyXmlApi>(GetUnsafeClient($"https://{host}", session), new RefitSettings
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

    public Task<WirelessDisplay> GetWirelessDisplayAsync(CancellationToken cancellationToken = default)
        => _envoyjsonclient.GetWirelessDisplayAsync(cancellationToken);

    public Task<WirelessDisplayExtended> GetWirelessDisplayExtendedAsync(CancellationToken cancellationToken = default)
        => _envoyjsonclient.GetWirelessDisplayExtendedAsync(cancellationToken);

    public Task<Home> GetHome(CancellationToken cancellationToken = default)
        => _envoyjsonclient.GetHome(cancellationToken);

    // Returns an HttpClient that ignores SSL errors since Envoy uses self-signed certificates
    private static HttpClient GetUnsafeClient(string baseAddress, EnvoySession session)
    {
        var client = new HttpClient(new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, sslErrors) => true,
            UseCookies = false
        })
        {
            BaseAddress = new Uri(baseAddress),
        };
        if (!string.IsNullOrEmpty(session.Token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.Token);
        }
        if (!string.IsNullOrEmpty(session.Id))
        {
            //client.DefaultRequestHeaders.Add("Cookie", $"sessionId={session.Id}");
        }   

        return client;
    }
}