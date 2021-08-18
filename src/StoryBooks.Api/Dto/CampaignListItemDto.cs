using System;
using StoryBooks.Models;

namespace StoryBooks.Api.Dto
{
    public class CampaignListItemDto
    {
        public Guid Id { get; }
        
        public string PartitionKey { get; }

        public string Name { get; }

        public CampaignStatus Status { get; }

        public DateTime CreationDate { get; }

        public DateTime ModificationDate { get; }

        public CampaignListItemDto(Campaign campaignDto)
        {
            PartitionKey = campaignDto.PartitionKey;
            Id = Guid.Parse(campaignDto.Id);
            Name = campaignDto.Name;
            Status = campaignDto.Status;
            CreationDate = campaignDto.CreationDate;
            ModificationDate = campaignDto.ModificationDate;
        }
    }
}