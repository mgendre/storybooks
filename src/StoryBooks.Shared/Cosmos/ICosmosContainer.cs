using Microsoft.Azure.Cosmos;

namespace StoryBooks.Shared.Cosmos
{
    public interface ICosmosContainer
    {
        public Container Container { get; }
    }
}