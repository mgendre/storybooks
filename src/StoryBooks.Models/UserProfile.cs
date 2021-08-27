using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StoryBooks.Models
{
    public class UserProfile : IModelBase
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string PartitionKey => Email;
        
        public string Issuer { get; set; } = string.Empty;
        public string SubjectId { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public IList<string> CampaignIds { get; set; } = new List<string>();
        
        public string LastName { get; set; } = string.Empty;
        
        public string FirstName { get; set; } = string.Empty;
        
        public DateTime CreationDate { get; set; }
        
        public DateTime ModificationDate { get; set; }
    }
}
