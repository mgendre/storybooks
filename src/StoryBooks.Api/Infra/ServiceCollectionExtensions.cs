using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using StoryBooks.Api.Infra.CosmosDb;
using StoryBooks.Api.Infra.CosmosDb.Containers;
using StoryBooks.Api.Repository;
using StoryBooks.Models;

namespace StoryBooks.Api.Infra
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Register a singleton instance of Cosmos Db Container Factory, which is a wrapper for the CosmosClient.
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddCosmosDb(this IServiceCollection services, CosmosDbSettings settings)
        {
            InitializeCosmos(services, settings).GetAwaiter().GetResult();
            return services;
        }


        /// <summary>
        /// Creates a Cosmos DB database and if required create containers
        /// </summary>
        /// <returns></returns>
        private static async Task InitializeCosmos(IServiceCollection services, CosmosDbSettings settings)
        {
            var databaseName = settings.DatabaseName;
            var endpoint = settings.EndpointUrl;
            var key = settings.PrimaryKey;
            var client = new CosmosClient(endpoint, key);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await Initialize(services, database);
        }

        private static async Task Initialize(IServiceCollection services, DatabaseResponse db)
        {
            var campaignContainer = new CampaignContainer(
                await db.Database.CreateContainerIfNotExistsAsync(
                    nameof(Campaign), "/PartitionKey")
            );
            services.AddSingleton(campaignContainer);
            services.AddSingleton<ICampaignRepository>(new CampaignRepository(campaignContainer));
            
            var userProfileContainer = new UserProfileContainer(
                await db.Database.CreateContainerIfNotExistsAsync(
                    nameof(UserProfile), "/PartitionKey")
            );
            services.AddSingleton(userProfileContainer);
            services.AddSingleton<IUserProfileRepository>(new UserProfileRepository(userProfileContainer));
        }
    }
}