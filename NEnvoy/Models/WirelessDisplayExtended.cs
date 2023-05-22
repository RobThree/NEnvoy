namespace NEnvoy.Models;

public record WirelessDisplayExtended(
  bool Supported,
  bool Present,
  bool Configured,
  bool Up,
  bool Carrier,
  WirelessNetwork CurrentNetwork,
  DeviceInfo DeviceInfo,
  string SelectedRegion,
  IEnumerable<string> Regions,
  IEnumerable<Site> Sites
) : WirelessDisplay(Supported, Present, Configured, Up, Carrier, CurrentNetwork, DeviceInfo, SelectedRegion, Regions);
