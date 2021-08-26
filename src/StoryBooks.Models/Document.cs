using System;
using Newtonsoft.Json;

namespace StoryBooks.Models
{
    public class Document : IModelBase
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string PartitionKey => CampaignId;
        
        public string CampaignId { get; set; }
        
        public string Filename { get; set; }
        
        public string MediaType { get; set; }

        public DateTime CreationDate { get; set; }
        
        public DateTime ModificationDate { get; set; }
    }
}
