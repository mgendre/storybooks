using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using StoryBooks.Api.Infra.CosmosDb.Containers;
using StoryBooks.Models;

namespace StoryBooks.Api.Repository
{
    public class CampaignRepository : AbstractCosmosRepository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(ICosmosContainer container) : base(container.Container)
        {
        }

        public async Task<Campaign> GetById(string id, CancellationToken cancellationToken)
        {
            return await GetById(id, new PartitionKey(id), cancellationToken);
        }
    }
}
