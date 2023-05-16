using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace NEnvoy.Models;

public record EnvoyDeviceBuildInfo
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [XmlElement("build_time_gmt")]
    public string __time
    {
        get => Time.ToUnixTimeSeconds().ToString();
        set => Time = DateTimeOffset.FromUnixTimeSeconds(int.Parse(value));
    }
    [XmlIgnore]
    public required DateTimeOffset Time { get; set; }

    [XmlElement("build_id")]
    public required string Id { get; init; }
}
