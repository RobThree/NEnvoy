using System.Xml;
using System.Xml.Serialization;

namespace NEnvoy.Models;

public record EnvoyDevice
{
    [XmlElement("sn")]
    public required string Serial { get; init; }
    [XmlElement("pn")]
    public required string ProductNumber { get; init; }

    [XmlElement("software")]
    public required string Software { get; init; }

    [XmlElement("euaid")]
    public required string EUAId { get; init; }
    [XmlElement("seqnum")]
    public required int SequenceNumber { get; init; }
    [XmlElement("apiver")]
    public required int ApiVerion { get; init; }
    [XmlElement("imeter")]
    public required bool IMeter { get; init; }
}
