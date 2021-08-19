using System;
using System.Collections.Generic;
using System.Linq;
using StoryBooks.Models;

namespace StoryBooks.Api.Dto
{
    public class CampaignDto : IModelBase
    {
        public string Id { get; set; } = "";

        public string Name { get; set; } = "";

        public CampaignStatus Status { get; set; } = CampaignStatus.InProgress;
        
        public DateTime CreationDate { get; set; }
        
        public DateTime ModificationDate { get; set; }

        public IReadOnlyCollection<ScenarioDto> Scenarios { get; set; } = new List<ScenarioDto>();
        
        public static CampaignDto FromModel(Campaign campaign)
        {
            return new CampaignDto
            {
                Id = campaign.Id,
                Name = campaign.Name,
                Status = campaign.Status,
                CreationDate = campaign.CreationDate,
                ModificationDate = campaign.ModificationDate,
                Scenarios = campaign.Scenarios.ToList().Select(ScenarioDto.FromModel).ToList()
            };
        }
    }
}
