using Microsoft.Azure.Cosmos;

namespace StoryBooks.Api.Business.Repository
{
    public class CampaignRepository : AbstractCosmosRepository<Models.Campaign>, ICampaignRepository
    {
        public CampaignRepository(Container container) : base(container)
        {
        }
    }
}
