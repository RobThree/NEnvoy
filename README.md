# ![Logo](https://raw.githubusercontent.com/RobThree/NEnvoy/master/logo.png) NEnvoy

![Build Status](https://img.shields.io/github/actions/workflow/status/RobThree/NEnvoy/test.yml?branch=master&style=flat-square) [![Nuget version](https://img.shields.io/nuget/v/NEnvoy.svg?style=flat-square)](https://www.nuget.org/packages/NEnvoy/)


A .Net Enphase Envoy client available as a [NuGet package](https://www.nuget.org/packages/NEnvoy).

Currently a work in progress. The list of currently implemented (and not yet implemented) items can be found [here](TODO.md).

## Quickstart

```c#
// Create client and login (assumes "envoy" hostname)
var ci = new EnvoyConnectionInfo
{
	Username = "user@gmail.com",
	Password = "sup3rs3cet",
};
var client = await EnvoyClient.FromLoginAsync(ci).ConfigureAwait(false);

// Get information from Envoy
var deviceinfo = await client.GetEnvoyInfoAsync();
```

### Sessions

A login is slow; once a session has been set up (logged in successfully) you can obtain the token and save it for later use:

```c#
var token = client.GetToken();
// Save the token to a file/database/whatever
```

You can then create client from the token:

```c#
var client = EnvoyClient.FromToken(token, ci);
```

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