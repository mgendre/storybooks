using System.Threading;
using System.Threading.Tasks;
using StoryBooks.Models;

namespace StoryBooks.Api.Repository
{
    public interface IUserProfileRepository : ICosmosRepository<UserProfile>
    {
        public Task<UserProfile> GetProfile(string email, CancellationToken cancellationToken);
    }
}