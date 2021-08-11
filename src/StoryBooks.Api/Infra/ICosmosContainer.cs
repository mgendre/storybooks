using Microsoft.Azure.Cosmos;

namespace StoryBooks.Api.Infra
{
    public interface ICosmosContainer
    {
        public Container Container { get; }
    }
}