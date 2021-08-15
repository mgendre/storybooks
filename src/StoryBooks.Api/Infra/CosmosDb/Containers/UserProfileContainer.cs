using Microsoft.Azure.Cosmos;

namespace StoryBooks.Api.Infra.CosmosDb.Containers
{
    public class UserProfileContainer: ICosmosContainer
    {
        public UserProfileContainer(Container container)
        {
            Container = container;
        }

        public Container Container { get; }
    }
}