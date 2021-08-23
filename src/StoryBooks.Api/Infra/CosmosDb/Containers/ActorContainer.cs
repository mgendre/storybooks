using Microsoft.Azure.Cosmos;

namespace StoryBooks.Api.Infra.CosmosDb.Containers
{
    public class ActorContainer: ICosmosContainer
    {
        public ActorContainer(Container container)
        {
            Container = container;
        }

        public Container Container { get; }
    }
}
