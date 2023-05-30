using System.Collections.Immutable;
using System.Text.Json.Nodes;

namespace NEnvoy.Models;
public record DeviceStatus
(
    DeviceStatusCounters Counters,
    ImmutableDictionary<string, ImmutableDictionary<string, JsonValue>>? PCU,   // TODO: What do these stand for exactly?
    ImmutableDictionary<string, ImmutableDictionary<string, JsonValue>>? ACB,   // TODO: What do these stand for exactly?
    ImmutableDictionary<string, ImmutableDictionary<string, JsonValue>>? NSRB,  // TODO: What do these stand for exactly?
    ImmutableDictionary<string, ImmutableDictionary<string, JsonValue>>? PLD,   // TODO: What do these stand for exactly?
    ImmutableDictionary<string, ImmutableDictionary<string, JsonValue>>? ESUB   // TODO: What do these stand for exactly?
);
