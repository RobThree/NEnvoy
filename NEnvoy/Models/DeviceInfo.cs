using System.Text.Json.Serialization;

namespace NEnvoy.Models;

public record DeviceInfo(
  [property: JsonPropertyName("vendor")] string Vendor,
  [property: JsonPropertyName("device")] string Device,
  [property: JsonPropertyName("vendor_id")] string VendorId,
  [property: JsonPropertyName("device_id")] string DeviceId,
  [property: JsonPropertyName("manufacturer")] string Manufacturer,
  [property: JsonPropertyName("model")] string Model,
  [property: JsonPropertyName("serial")] string Serial,
  [property: JsonPropertyName("hw_version")] string HardwareVersion,
  [property: JsonPropertyName("usb_spec")] string USBSpec,
  [property: JsonPropertyName("usb_slot")] string USBSlot,
  [property: JsonPropertyName("driver")] string Driver,
  [property: JsonPropertyName("mac")] string MacAddress
);