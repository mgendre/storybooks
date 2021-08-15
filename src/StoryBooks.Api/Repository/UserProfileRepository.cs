using StoryBooks.Api.Infra.CosmosDb.Containers;

namespace StoryBooks.Api.Repository
{
    public class UserProfileRepository : AbstractCosmosRepository<Models.UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(UserProfileContainer container) : base(container.Container)
        {
        }
    }
}
