using Newtonsoft.Json;

namespace Lab5.Models
{
    public class MiataruGetLocationDevice
    {
        [JsonProperty("Device")]
        public string Device { get; set; }

        [JsonProperty("MiataruConfig")]
        public MiataruConfig Config { get; set; }
    }

    public class MiataruGetLocationRequest
    {
        [JsonProperty("MiataruGetLocation")]
        public List<MiataruGetLocationDevice> Devices { get; set; }
    }

    public class MiataruConfig
    {
        [JsonProperty("RequestMiataruDeviceID")]
        public string RequestMiataruDeviceID { get; set; }
    }

}
