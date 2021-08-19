using System;
using Newtonsoft.Json;

namespace StoryBooks.Models
{
    public class Scenario : IModelBase
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Campaign { get; set; } = "";

        public string PartitionKey => Id;
        
        public DateTime CreationDate { get; set; }
        
        public DateTime ModificationDate { get; set; }
        public string Title { get; set; }
        public string Markdown { get; set; }
    }
}