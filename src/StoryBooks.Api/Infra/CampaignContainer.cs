using Microsoft.Azure.Cosmos;

namespace StoryBooks.Api.Infra
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