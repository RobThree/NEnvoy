using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace NEnvoy.Models;

[XmlRoot("envoy_info")]
public record EnvoyInfo
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [XmlElement("time")]
    public string __time
    {
        get => Time.ToUnixTimeSeconds().ToString();
        set => Time = DateTimeOffset.FromUnixTimeSeconds(int.Parse(value));
    }
    [XmlIgnore]
    public required DateTimeOffset Time { get; set; }

    [XmlElement("device")]
    public required EnvoyDevice Device { get; init; }
    [XmlElement("web-tokens")]
    public required bool WebTokens { get; init; }
    [XmlElement("package")]
    public required EnvoyPackage[] Packages { get; init; }
    [XmlElement("build_info")]
    public required EnvoyDeviceBuildInfo BuildInfo { get; init; }
}
