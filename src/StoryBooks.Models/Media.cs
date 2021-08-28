using System;
using Newtonsoft.Json;

namespace StoryBooks.Models
{
    public class Media : IModelBase
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string PartitionKey => CampaignId;

        public string CampaignId { get; set; } = string.Empty;
        
        public string? Label { get; set; }

        public MediaStorageType StorageType { get; set; } = MediaStorageType.Document;
        
        public Uri? ExternalUri { get; set; }
        
        public Document? Document { get; set; }

        public DateTime CreationDate { get; set; }
        
        public DateTime ModificationDate { get; set; }
    }

    public enum MediaStorageType
    {
        ExternalUri, Document
    }
}
