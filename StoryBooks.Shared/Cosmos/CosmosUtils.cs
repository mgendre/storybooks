using Microsoft.Azure.Cosmos;

namespace StoryBooks.Shared.Cosmos
{
    public class CosmosUtils
    {
        public static CosmosClient  CreateClient(CosmosDbSettings settings)
        {
            var endpoint = settings.EndpointUrl;
            var key = settings.PrimaryKey;
            return new CosmosClient(endpoint, key);
        }
    }
}