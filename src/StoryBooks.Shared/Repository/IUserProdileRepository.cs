using System.Threading;
using System.Threading.Tasks;

namespace StoryBooks.Shared.Repository
{
    public interface IUserProfileRepository : ICosmosRepository<Models.UserProfile>
    {
        public Task<Models.UserProfile> GetProfile(string email, CancellationToken cancellationToken);
    }
}