using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using StoryBooks.Models;

namespace StoryBooks.Api.Infra.CosmosDb
{
    public static class CosmosDbExtensions
    {
        public static async Task<IReadOnlyList<T>> ToListAsync<T>(this FeedIterator<T> iterator, CancellationToken ct) where T : IModelBase
        {
            var results = new List<T>();

            while (iterator.HasMoreResults)
            {
                var currentResultSet = await iterator.ReadNextAsync(ct);
                results.AddRange(currentResultSet.Resource);
            }
            return results;
        }
        
        public static async Task<T?> FirstOrDefaultAsync<T>(this FeedIterator<T> iterator, CancellationToken ct) where T : IModelBase
        {
            while (iterator.HasMoreResults)
            {
                var currentResultSet = await iterator.ReadNextAsync(ct);
                return currentResultSet.FirstOrDefault();
            }

            return default;
        }
        
        public static async Task<T> FirstAsync<T>(this FeedIterator<T> iterator, CancellationToken ct) where T : IModelBase
        {
            while (iterator.HasMoreResults)
            {
                var currentResultSet = await iterator.ReadNextAsync(ct);
                return currentResultSet.First();
            }

            throw new InvalidOperationException("Cosmos response doesn't contain any element");
        }
    }
}