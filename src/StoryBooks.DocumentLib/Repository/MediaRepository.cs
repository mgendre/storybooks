using StoryBooks.DocumentLib.Infra;
using StoryBooks.Models;
using StoryBooks.Shared.Repository;

namespace StoryBooks.DocumentLib.Repository
{
    public class MediaRepository : AbstractCosmosRepository<Media>, IMediaRepository
    {
        public MediaRepository(MediaContainer container) : base(container.Container)
        {
        }
    }
}
