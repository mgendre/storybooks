using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StoryBooks.Models
{
    public class Campaign : IModelBase
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string PartitionKey => Id;
        
        public string Name { get; set; } = string.Empty;

        [JsonConverter(typeof(StringEnumConverter))]
        public CampaignStatus Status { get; set; }

        public DateTime CreationDate { get; set; }
        
        public DateTime ModificationDate { get; set; }
    }
}
