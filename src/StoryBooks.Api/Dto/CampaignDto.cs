using System;
using StoryBooks.Models;

namespace StoryBooks.Api.Dto
{
    public class CampaignDto : IModelBase
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public CampaignStatus Status { get; set; } = CampaignStatus.InProgress;
        
        public DateTime CreationDate { get; set; }
        
        public DateTime ModificationDate { get; set; }

        public static CampaignDto FromModel(Campaign campaign)
        {
            return new CampaignDto
            {
                Id = campaign.Id,
                Name = campaign.Name,
                Status = campaign.Status,
                CreationDate = campaign.CreationDate,
                ModificationDate = campaign.ModificationDate
            };
        }
    }
}
