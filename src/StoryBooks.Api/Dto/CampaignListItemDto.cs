using System;
using StoryBooks.Models;

namespace StoryBooks.Api.Dto
{
    public class CampaignListItemDto
    {
        public Guid Id { get; }

        public string Name { get; }

        public CampaignStatus Status { get; }

        public DateTime CreationDate { get; }

        public DateTime ModificationDate { get; }

        public CampaignListItemDto(Campaign campaign)
        {
            Id = Guid.Parse(campaign.Id);
            Name = campaign.Name;
            Status = campaign.Status;
            CreationDate = campaign.CreationDate;
            ModificationDate = campaign.ModificationDate;
        }
    }
}