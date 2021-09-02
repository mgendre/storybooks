using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using StoryBooks.Shared.Cosmos;
using StoryBooks.Shared.Exceptions;

namespace StoryBooks.Shared.Repository
{
    public class UserProfileRepository : AbstractCosmosRepository<Models.UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(UserProfileContainer container) : base(container.Container)
        {
        }

        public async Task<Models.UserProfile> GetProfile(string email, CancellationToken cancellationToken)
        {
            // Partition is by email, we search the first element from the partition
            var options = new QueryRequestOptions
            {
                PartitionKey = new PartitionKey(email)
            };
            var it = Container.GetItemQueryIterator<Models.UserProfile>(requestOptions: options);
            var collection = await it.ToListAsync(cancellationToken);
            return collection.FirstOrDefault() ?? throw new DataNotFoundException("Could not find profile for email " + email);
        }
    }
}
