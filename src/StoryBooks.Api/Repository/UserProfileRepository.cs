using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using StoryBooks.Api.Infra.CosmosDb;
using StoryBooks.Api.Infra.CosmosDb.Containers;
using StoryBooks.Api.Infra.Exceptions;
using StoryBooks.Models;

namespace StoryBooks.Api.Repository
{
    public class UserProfileRepository : AbstractCosmosRepository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(ICosmosContainer container) : base(container.Container)
        {
        }

        public async Task<UserProfile> GetProfile(string email, CancellationToken cancellationToken)
        {
            // Partition is by email, we search the first element from the partition
            var options = new QueryRequestOptions
            {
                PartitionKey = new PartitionKey(email)
            };
            var it = Container.GetItemQueryIterator<UserProfile>(requestOptions: options);
            var collection = await it.ToListAsync(cancellationToken);
            return collection.FirstOrDefault() ?? throw new DataNotFoundException("Could not find profile for email " + email);
        }
    }
}
