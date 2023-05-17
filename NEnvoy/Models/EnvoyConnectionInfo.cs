namespace NEnvoy.Models;

public record EnvoyConnectionInfo(
    string Username = "",
    string Password = "",
    string EnvoyHost = "envoy",
    string EnphaseBaseUri = EnvoyClient.DefaultEnphaseBaseUri,
    string EnphaseEntrezBaseUri = EnvoyClient.DefaultEntrezBaseUri
);