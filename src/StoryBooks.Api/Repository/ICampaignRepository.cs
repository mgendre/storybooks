using System.Threading;
using System.Threading.Tasks;
using StoryBooks.Models;

namespace StoryBooks.Api.Repository
{
    public interface ICampaignRepository : ICosmosRepository<Models.Campaign>
    {
        public Task<Campaign> GetById(string id, CancellationToken cancellationToken);
    }
}