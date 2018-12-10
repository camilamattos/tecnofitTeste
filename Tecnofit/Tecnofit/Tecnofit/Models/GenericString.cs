using Newtonsoft.Json;

namespace Tecnofit.Models
{
    public class GenericString
    {
        [JsonProperty("token")]
        public string token { get; set; }
    }
}
