using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StoryBooks.Models
{
    public class UserProfile : IModelBase
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string PartitionKey => Email;

        public string Email { get; set; }

        public Campaign[] Campaigns { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public DateTime ModificationDate { get; set; }
    }
}
