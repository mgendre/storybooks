using StoryBooks.Models;

namespace StoryBooks.Api.Business.Campaign
{
    public class CampaignListItemDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public CampaignStatus Status { get; set; }
    }
}