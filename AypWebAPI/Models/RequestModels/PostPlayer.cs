using Newtonsoft.Json;

namespace AypWebAPI.Models.RequestModels
{
    public class PostPlayer
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BackNumber { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? TeamId { get; set; }
    }
}
