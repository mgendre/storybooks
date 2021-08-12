using Microsoft.Azure.Cosmos;

namespace StoryBooks.Api.Infra.CosmosDb.Containers
{
    public interface ICosmosContainer
    {
        public Container Container { get; }
    }
}