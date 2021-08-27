using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StoryBooks.Models
{
    public class Scenario : IModelBase
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string CampaignId { get; set; } = string.Empty;
        public string PartitionKey => CampaignId;
        
        public DateTime CreationDate { get; set; }
        
        public DateTime ModificationDate { get; set; }
        public string Title { get; set; } = string.Empty;

        public IList<Section> Sections { get; set; } = Array.Empty<Section>();
    }

    public class Section
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string Markdown { get; set; } = string.Empty;
    }
}