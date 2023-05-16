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

    private readonly IEnvoy _envoyclient;

    public string SessionToken { get; private set; }
    public bool IsConsumer { get; private set; }

    private EnvoyClient(IEnvoy envoyClient, string sessionToken, bool isconsumer)
    {
        _envoyclient = envoyClient ?? throw new ArgumentNullException(nameof(envoyClient));
        SessionToken = sessionToken ?? throw new ArgumentNullException(nameof(sessionToken));
        IsConsumer = isconsumer;
    }

    public static EnvoyClient FromSessionToken(string host, string sessionToken, bool isConsumer = true)
        => new(GetEnvoyClient(host, sessionToken), sessionToken, isConsumer);

    public static async Task<EnvoyClient> FromLoginAsync(ConnectionInfo connectionInfo, CancellationToken cancellationToken = default)
    {
        var envoyxmlclient = GetEnvoyClient(connectionInfo.EnvoyHost, null, new RefitSettings
        {
            ContentSerializer = new XmlContentSerializer()
        });
        var envoyInfo = await GetEnvoyInfoAsync(envoyxmlclient, cancellationToken).ConfigureAwait(false);

        var enphaseclient = RestService.For<IEnphase>(connectionInfo.EnphaseBaseUri);
        var loginresult = await enphaseclient.LoginAsync(new EnphaseLoginRequest(connectionInfo.Username, connectionInfo.Password), cancellationToken).ConfigureAwait(false);
        if (string.Equals(loginresult.Message, "success", StringComparison.OrdinalIgnoreCase))
        {
            var entrezclient = RestService.For<IEntrezEnphase>(connectionInfo.EnphaseEntrezBaseUri);
            var token = await entrezclient.RequestTokenAsync(new EnphaseTokenRequest(loginresult.SessionId, envoyInfo.Device.Serial, connectionInfo.Username), cancellationToken).ConfigureAwait(false);
            var envoyjsonclient = GetEnvoyClient(connectionInfo.EnvoyHost, token);
            return new EnvoyClient(envoyjsonclient, token, loginresult.IsConsumer);
        }
        throw new LoginFailedException(loginresult.Message);
    }

    private static IEnvoy GetEnvoyClient(string host, string? sessionToken, RefitSettings? refitSettings = null)
        => RestService.For<IEnvoy>(GetUnsafeClient($"https://{host}", sessionToken), refitSettings);

    private static Task<EnvoyInfo> GetEnvoyInfoAsync(IEnvoy envoyClient, CancellationToken cancellationToken = default)
        => envoyClient.GetDeviceInfoAsync(cancellationToken);

    public Task<EnvoyInfo> GetEnvoyInfoAsync(CancellationToken cancellationToken = default)
        => _envoyclient.GetDeviceInfoAsync(cancellationToken);

    public Task<IEnumerable<ConsumptionReport>> GetConsumptionAsync(CancellationToken cancellationToken = default)
        => _envoyclient.GetConsumptionAsync(cancellationToken);

    public Task<IEnumerable<V1Inverter>> GetV1InvertersAsync(CancellationToken cancellationToken = default)
        => _envoyclient.GetV1InvertersAsync(cancellationToken);

    public Task<V1Production> GetV1ProductionAsync(CancellationToken cancellationToken = default)
        => _envoyclient.GetV1ProductionAsync(cancellationToken);

    public Task<IEnumerable<Meter>> GetMetersAsync(CancellationToken cancellationToken = default)
        => _envoyclient.GetMetersAsync(cancellationToken);

    public Task<IEnumerable<RootMeterReading>> GetMeterReadingsAsync(CancellationToken cancellationToken = default)
        => _envoyclient.GetMeterReadingsAsync(cancellationToken);

    public Task<WirelessDisplay> GetWirelessDisplayAsync(CancellationToken cancellationToken = default)
        => _envoyclient.GetWirelessDisplayAsync(cancellationToken);

    public Task<WirelessDisplayExtended> GetWirelessDisplayExtendedAsync(CancellationToken cancellationToken = default)
        => _envoyclient.GetWirelessDisplayExtendedAsync(cancellationToken);

    public Task<Home> GetHome(CancellationToken cancellationToken = default)
        => _envoyclient.GetHome(cancellationToken);

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