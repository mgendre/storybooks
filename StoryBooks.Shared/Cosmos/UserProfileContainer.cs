using Microsoft.Azure.Cosmos;

namespace StoryBooks.Shared.Cosmos
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