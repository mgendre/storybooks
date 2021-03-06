using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StoryBooks.Models;
using StoryBooks.Shared.Repository;

namespace StoryBooks.Api.Repository
{
    public interface IActorRepository<T> : ICosmosRepository<T> where T : AbstractActor
    {
        public Task<IReadOnlyCollection<T>> FindAll(string campaignId, CancellationToken ct);
    }
}
