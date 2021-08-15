using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using StoryBooks.Models;

namespace StoryBooks.Api.Repository
{
    public abstract class AbstractCosmosRepository<T> : ICosmosRepository<T> where T : IModelBase
    {
        public Container Container { get; }

        protected AbstractCosmosRepository(Container container)
        {
            Container = container;
        }
        
        public async Task<T> GetById(string id, PartitionKey key, CancellationToken ct)
        {
            var existing = await Container.ReadItemAsync<T>(
                id, key, cancellationToken: ct);
            return existing.Resource;
        }

        public async Task<T> Create(T toCreate, CancellationToken ct)
        {
            var response = await Container.CreateItemAsync(toCreate, cancellationToken: ct);
            return response.Resource;
        }

        public async Task<T> Update(string id, PartitionKey key, Action<T> patch, CancellationToken ct)
        {
            var existing = await GetById(id, key, ct);
            patch(existing);
            
            var result = await Container.UpsertItemAsync(existing, key, cancellationToken: ct);
            return result.Resource;
        }

        public async Task Delete(T toDelete, PartitionKey key, CancellationToken ct)
        {
            await Container.DeleteItemAsync<T>(toDelete.Id, key, cancellationToken: ct);
        }
    }
}