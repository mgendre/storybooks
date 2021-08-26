using Microsoft.Azure.Cosmos;

namespace StoryBooks.Shared.Cosmos
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