using Microsoft.Azure.Cosmos;
using StoryBooks.Shared.Cosmos;

namespace StoryBooks.DocumentLib.Infra
{
    public class MediaContainer: ICosmosContainer
    {
        public MediaContainer(Container container)
        {
            Container = container;
        }

        public Container Container { get; }
    }
}