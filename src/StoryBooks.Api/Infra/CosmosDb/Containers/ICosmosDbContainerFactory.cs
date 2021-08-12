using Microsoft.Azure.Cosmos;

namespace StoryBooks.Api.Infra.CosmosDb.Containers
{
    public interface ICosmosDbContainerFactory
    {
        public interface ICosmosDbContainer
        {
            /// <summary>
            ///     Azure Cosmos DB Container
            /// </summary>
            Container Container { get; }
        }
    }
}