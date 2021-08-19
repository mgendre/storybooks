using Microsoft.Azure.Cosmos;

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