using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using StoryBooks.Api.Infra.CosmosDb;
using StoryBooks.Api.Infra.CosmosDb.Containers;
using StoryBooks.Models;

namespace StoryBooks.Api.Repository
{
    public class ActorRepository<T> : AbstractCosmosRepository<T>, IActorRepository<T> where T : AbstractActor
    {
        public ActorRepository(ICosmosContainer container) : base(container.Container)
        {
        }

        public async Task<IReadOnlyCollection<T>> FindAll(string campaignId, CancellationToken ct)
        {
            var options = new QueryRequestOptions
            {
                PartitionKey = new PartitionKey(campaignId)
            };
            QueryDefinition queryByType = new QueryDefinition("select * from actor where actor.Type = @Type")
                .WithParameter("@Type", GetActorType());
            var iterator = Container.GetItemQueryIterator<T>(queryByType, requestOptions: options);
            return await iterator.ToListAsync(ct);
        }

        public override Task<T> Create(T toCreate, CancellationToken ct)
        {
            toCreate.Type = GetActorType();
            return base.Create(toCreate, ct);
        }

        protected virtual string GetActorType()
        {
            return typeof(T).Name;
        }
    }
}
