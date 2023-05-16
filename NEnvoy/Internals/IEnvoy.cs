﻿using NEnvoy.Internals.Models;
using NEnvoy.Models;
using Refit;

namespace NEnvoy.Internals;

internal interface IEnvoy
{
    [Get("/info.xml")]
    Task<EnvoyInfo> GetDeviceInfoAsync(CancellationToken cancellationToken = default);

    [Get("/ivp/meters/reports/consumption")]
    Task<IEnumerable<ConsumptionReport>> GetConsumptionAsync(CancellationToken cancellationToken);

    [Get("/api/v1/production")]
    Task<V1Production> GetV1ProductionAsync(CancellationToken cancellationToken);

    [Get("/api/v1/production/inverters")]
    Task<IEnumerable<V1Inverter>> GetV1InvertersAsync(CancellationToken cancellationToken);

    [Get("/ivp/meters")]
    Task<IEnumerable<Meter>> GetMetersAsync(CancellationToken cancellationToken = default);

    [Get("/ivp/meters/readings")]
    Task<IEnumerable<RootMeterReading>> GetMeterReadingsAsync(CancellationToken cancellationToken = default);

    [Get("/admin/lib/wireless_display.json?site_info=0")]
    Task<WirelessDisplay> GetWirelessDisplayAsync(CancellationToken cancellationToken = default);

}
