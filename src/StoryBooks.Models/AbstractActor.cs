using System;
using Newtonsoft.Json;

namespace StoryBooks.Models
{
    public abstract class AbstractActor : IModelBase
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string PartitionKey => CampaignId;
        public string CampaignId { get; set; } = string.Empty;

        public virtual string Name { get; set; } = string.Empty;

        public string DescriptionMarkdown { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        
        public string Type { get; set; } = string.Empty;
        
        public string? PortraitMediaId { get; set; }
    }
}
