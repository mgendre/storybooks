using System;
using StoryBooks.Models;

namespace StoryBooks.DocumentLib.Dto
{
    public class MediaDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string CampaignId { get; set; } = string.Empty;

        public MediaStorageType StorageType { get; set; } = MediaStorageType.Document;
        
        public Uri? ExternalUri { get; set; }
        
        public string? DocumentId { get; set; }
        
        public string? Label { get; set; }

        public DateTime CreationDate { get; set; }
        
        public DateTime ModificationDate { get; set; }

        public static MediaDto FromModel(Media media)
        {
            return new MediaDto
            {
                Id = media.Id,
                CampaignId = media.CampaignId,
                CreationDate = media.CreationDate,
                ModificationDate = media.ModificationDate,
                DocumentId = media.Document?.Id,
                ExternalUri = media.ExternalUri,
                StorageType = media.StorageType,
                Label = media.Label
            };
        }
    }
}
