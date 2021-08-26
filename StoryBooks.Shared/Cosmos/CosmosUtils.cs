using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace StoryBooks.Shared.Cosmos
{
    public class CosmosUtils
    {
        public static async Task<DatabaseResponse> CreateDataBaseIfNotExists(CosmosDbSettings settings)
        {
            var databaseName = settings.DatabaseName;
            var endpoint = settings.EndpointUrl;
            var key = settings.PrimaryKey;
            var client = new CosmosClient(endpoint, key);
            return await client.CreateDatabaseIfNotExistsAsync(databaseName);
        }
    }
}