using StoryBooks.Models;
using StoryBooks.Shared.Repository;

namespace StoryBooks.DocumentLib.Repository
{
    public interface IMediaRepository : ICosmosRepository<Media>
    {
    }
}