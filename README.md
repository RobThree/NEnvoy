# ![Logo](logo.png) NEnvoy

A .Net Enphase Envoy client. Currently a work in progress.

## Quickstart

```c#
// Create client and login (assumes "envoy" hostname)
var ci = new ConnectionInfo("user@gmail.com", "sup3rs3cet");
var client = await EnvoyClient.FromLoginAsync(ci).ConfigureAwait(false);

// Get information from Envoy
var deviceinfo = await client.GetEnvoyInfoAsync();
```

## Connection

You can specify a different hostname or IP address for your envoy:

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

## License

Licensed under MIT license. See [LICENSE](LICENSE) for details.

---

Icon made by [Payungkead](https://www.flaticon.com/authors/payungkead) from [www.flaticon.com](http://www.flaticon.com/) is licensed by [CC 3.0](http://creativecommons.org/licenses/by/3.0/).