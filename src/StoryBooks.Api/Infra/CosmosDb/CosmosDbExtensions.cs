using System.Collections.Generic;
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
    }
}