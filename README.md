# ![Logo](logo.png) NEnvoy

A .Net Enphase Envoy client available as a [NuGet package](https://www.nuget.org/packages/NEnvoy).

Currently a work in progress. The list of currently implemented (and not yet implemented) items can be found [here](TODO.md).

## Quickstart

```c#
// Create client and login (assumes "envoy" hostname)
var ci = new ConnectionInfo("user@gmail.com", "sup3rs3cet");
var client = await EnvoyClient.FromLoginAsync(ci).ConfigureAwait(false);

// Get information from Envoy
var deviceinfo = await client.GetEnvoyInfoAsync();
```

### Sessions

A login can be slow; once a session has been set up (logged in successfully) you can obtain sessioninfo with the `GetSessionInfo()` method. The information in the returned `SessionInfo` object can then be stored where you want. Next time, the session can be 'resumed' (assuming it hasn't expired) as follows:

```c#
var session = new SessionInfo { Token = "eyJ...iJ9", Id = "i9O...p5Q" };
var client = EnvoyClient.FromSession(session);
// or, if you don't want to use the default "envoy" hostname:
var client = EnvoyClient.FromSession(session, "envoy.local");
// or:
var client = EnvoyClient.FromSession(session, "192.168.123.45");
```

Sessions can be stored in your `appsettings.json`, `secrets.json` or wherever you want. We provide a few conveniencemethods that save and load sessions to and from a file or stream:

```c#
Static methods on EnvoyClient:

// Save to and load from file:
Task SaveSessionAsync(path, sessionInfo, jsonSerializerOptions, cancellationToken);
Task<SessionInfo> LoadSessionAsync(path, jsonSerializerOptions, cancellationToken);

// Save to and load from stream:
Task SaveSessionAsync(stream, sessionInfo, jsonSerializerOptions, cancellationToken);
Task<SessionInfo> LoadSessionAsync(stream, jsonSerializerOptions, cancellationToken);

// Instance methods on EnvoyClient:
Task SaveSessionAsync(path, jsonSerializerOptions, cancellationToken);
Task SaveSessionAsync(stream, jsonSerializerOptions, cancellationToken);
```

If you'd rather store session information in another way, you can get the session info by calling `GetSessionInfo()`.

### Connection

You can specify a different hostname or IP address for your Envoy:

```c#
var ci = new ConnectionInfo("user@gmail.com", "sup3rs3cet", "envoy.local");
// or:
var ci = new ConnectionInfo("user@gmail.com", "sup3rs3cet", "192.168.123.45");
```

Similarly, the base URI's for the enphase portal can be overridden:

```c#
var ci = new ConnectionInfo("user@gmail.com", "sup3rs3cet", EnphaseBaseUri: "https://enlighten-new.enphaseenergy.com");
// or:
var ci = new ConnectionInfo("user@gmail.com", "sup3rs3cet", EnphaseEntrezBaseUri: "https://entrez-new.enphaseenergy.com");
```

## Contributing

Yes please! Implement any of the methods and make a PR and we'll take it from there.

## License

Licensed under MIT license. See [LICENSE](LICENSE) for details.

---

Icon made by [Payungkead](https://www.flaticon.com/authors/payungkead) from [www.flaticon.com](http://www.flaticon.com/) is licensed by [CC 3.0](http://creativecommons.org/licenses/by/3.0/).