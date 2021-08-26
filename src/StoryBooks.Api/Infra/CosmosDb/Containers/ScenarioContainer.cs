using Microsoft.Azure.Cosmos;
using StoryBooks.Shared.Cosmos;

namespace StoryBooks.Api.Infra.CosmosDb.Containers
{
    public class ScenarioContainer: ICosmosContainer
    {
        public ScenarioContainer(Container container)
        {
            Container = container;
        }

        public Container Container { get; }
    }
}