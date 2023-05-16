namespace NEnvoy.Models;

public record ConnectionInfo(
    string EnvoyHost = "Envoy",
    string Username = "",
    string Password = "",
    string EnphaseBaseUri = EnvoyClient.DefaultEnphaseBaseUri,
    string EnphaseEntrezBaseUri = EnvoyClient.DefaultEntrezBaseUri
);