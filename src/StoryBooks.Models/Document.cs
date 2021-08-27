using System;
using Newtonsoft.Json;

namespace StoryBooks.Models
{
    public class Document
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string BlobName => Id;

        public string Filename { get; set; } = string.Empty;

    }
}
