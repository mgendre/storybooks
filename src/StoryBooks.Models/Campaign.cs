using Newtonsoft.Json;

namespace StoryBooks.Models
{
    public class Campaign
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        
        public string PartitionKey { get; set; }

        public string Name { get; set; }

        public CampaignStatus Status { get; set; }
        
        public Story[] Stories { get; set; }
    }
}
