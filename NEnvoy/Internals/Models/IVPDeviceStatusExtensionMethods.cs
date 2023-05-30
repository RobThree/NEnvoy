using NEnvoy.Models;
using System.Collections.Immutable;
using System.Text.Json.Nodes;

namespace NEnvoy.Internals.Models;

internal static class IVPDeviceStatusExtensionMethods
{
    public static DeviceStatusCounter ToDeviceStatusCounter(this IVPDeviceStatusCounterValues values)
        => new(values.Expected, values.Discovered, values.CtrlsTotal, values.CtrlsGone, values.CtrlsCommunicating, values.ChannelsTotal, values.ChannelsRecent, values.ChannelsProducing);

    public static ImmutableDictionary<string, ImmutableDictionary<string, JsonValue>> ToDeviceStatusValues(this IVPDeviceStatusValues devicevalues, IEqualityComparer<string>? equalityComparer = null)
    {
        var comparer = equalityComparer ?? StringComparer.Ordinal;
        var fields = devicevalues.Fields.ToArray();
        var serialnumberindex = fields.Select((f, i) => (Field: f, Index: i)).Single(f => comparer.Equals(f.Field, "serialNumber")).Index;

        return devicevalues.DeviceValues.ToImmutableDictionary(
            dv => dv[serialnumberindex]?.ToString() ?? string.Empty,
            dv => fields.Select((f, i) => (Field: f, Index: i)).ToImmutableDictionary(f => f.Field, f => GetSafeItem(dv, f.Index), comparer),
            comparer
        );
    }

    private static JsonValue GetSafeItem(JsonArray array, int index)
        => index >= 0 && index < array.Count ? array[index]!.AsValue() : throw new IndexOutOfRangeException();  //TODO: Decent exception
}