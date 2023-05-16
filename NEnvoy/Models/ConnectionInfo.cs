namespace NEnvoy.Models;

public record ConnectionInfo(
    string Username = "",
    string Password = "",
    string EnvoyHost = "envoy",
    string EnphaseBaseUri = EnvoyClient.DefaultEnphaseBaseUri,
    string EnphaseEntrezBaseUri = EnvoyClient.DefaultEntrezBaseUri
);