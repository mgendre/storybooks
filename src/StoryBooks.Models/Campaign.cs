using Newtonsoft.Json;

namespace StoryBooks.Models
{
    public class Campaign
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "status")]
        public CampaignStatus Status { get; set; }
    }
}
