using StoryBooks.Api.Infra.CosmosDb.Containers;

namespace StoryBooks.Api.Repository
{
    public class CampaignRepository : AbstractCosmosRepository<Models.Campaign>, ICampaignRepository
    {
        public CampaignRepository(CampaignContainer container) : base(container.Container)
        {
        }
    }
}
