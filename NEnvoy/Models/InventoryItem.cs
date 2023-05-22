using System.Text.Json.Serialization;

namespace NEnvoy.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(PCUInventoryItem), typeDiscriminator: "PCU")]
[JsonDerivedType(typeof(ACBInventoryItem), typeDiscriminator: "ACB")]
[JsonDerivedType(typeof(NSRBInventoryItem), typeDiscriminator: "NSRB")]
[JsonDerivedType(typeof(ESUBInventoryItem), typeDiscriminator: "ESUB")]
public abstract record InventoryItem();
