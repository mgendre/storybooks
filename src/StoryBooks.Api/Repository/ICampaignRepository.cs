using System.Threading;
using System.Threading.Tasks;
using StoryBooks.Models;
using StoryBooks.Shared.Repository;

namespace StoryBooks.Api.Repository
{
    public interface ICampaignRepository : ICosmosRepository<Campaign>
    {
        public Task<Campaign> GetById(string id, CancellationToken cancellationToken);
    }
}