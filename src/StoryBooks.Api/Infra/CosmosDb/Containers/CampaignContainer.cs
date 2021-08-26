using Microsoft.Azure.Cosmos;
using StoryBooks.Shared.Cosmos;

namespace StoryBooks.Api.Infra.CosmosDb.Containers
{
    public class CampaignContainer: ICosmosContainer
    {
        public CampaignContainer(Container container)
        {
            Container = container;
        }

        public Container Container { get; }
    }
}