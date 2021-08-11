using Microsoft.Azure.Cosmos;

namespace StoryBooks.Api.Infra
{
    public class CosmosDbContainer : ICosmosDbContainerFactory.ICosmosDbContainer
    {
        public Container Container { get; }

        public CosmosDbContainer(CosmosClient cosmosClient,
            string databaseName,
            string containerName)
        {
            Container = cosmosClient.GetContainer(databaseName, containerName);
        }
    }
}
