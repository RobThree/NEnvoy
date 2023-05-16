using System.Xml;
using System.Xml.Serialization;

namespace NEnvoy.Models;

public record EnvoyPackage
{
    [XmlAttribute("name")]
    public required string Name { get; init; }
    [XmlElement("pn")]
    public required string ProductNumber { get; init; }
    [XmlElement("version")]
    public required string Version { get; init; }
    [XmlElement("build")]
    public required string Build { get; init; }
}