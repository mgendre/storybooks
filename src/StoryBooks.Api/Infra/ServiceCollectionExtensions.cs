using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using StoryBooks.Api.Infra.CosmosDb.Containers;
using StoryBooks.Api.Repository;
using StoryBooks.Models;
using StoryBooks.Shared.Cosmos;

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
            
            await Initialize(services, await CosmosUtils.CreateDataBaseIfNotExists(settings));
        }

        private static async Task Initialize(IServiceCollection services, DatabaseResponse db)
        {
            var campaignContainer = new CampaignContainer(
                await db.Database.CreateContainerIfNotExistsAsync(
                    nameof(Campaign), "/PartitionKey")
            );
            services.AddSingleton(campaignContainer);
            services.AddSingleton<ICampaignRepository, CampaignRepository>();
            
            var scenarioContainer = new ScenarioContainer(
                await db.Database.CreateContainerIfNotExistsAsync(
                    nameof(Scenario), "/PartitionKey")
            );
            services.AddSingleton(scenarioContainer);
            services.AddSingleton<IScenarioRepository, ScenarioRepository>();
            
            var actorContainer = new ActorContainer(
                await db.Database.CreateContainerIfNotExistsAsync(
                    "Actor", "/PartitionKey")
            );
            services.AddSingleton(actorContainer);
            services.AddSingleton<IActorRepository<Character>, ActorRepository<Character>>();
            services.AddSingleton<IActorRepository<Place>, ActorRepository<Place>>();
        }
    }
}