using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using StoryBooks.Models;

namespace StoryBooks.Api.Business.Repository
{
    public interface ICosmosRepository<T> where T: IModelBase
    {
        public Container Container { get; }
        public Task<T> Create(T toCreate, CancellationToken ct);
        public Task<T> GetById(string id, PartitionKey key, CancellationToken ct);
        public Task<T> Update(string id, PartitionKey key, Action<T> patch, CancellationToken ct);
        public Task Delete(T toDelete, PartitionKey key, CancellationToken ct);
    }
}
