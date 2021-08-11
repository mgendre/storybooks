using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
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

            var options = new CosmosClientOptions();
            if (endpoint.Contains("localhost"))
            {
                // We disable SSL check with the emulator
                options.HttpClientFactory = () =>
                {
                    HttpMessageHandler httpMessageHandler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (req, cert, chain, errors) => true
                    };
                    return new HttpClient(httpMessageHandler);
                };
                options.ConnectionMode = ConnectionMode.Gateway;
            }

            var client = new CosmosClient(endpoint, key, options);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);

            await InitializeContainers(services, database);
        }

        private static async Task InitializeContainers(IServiceCollection services, DatabaseResponse db)
        {
            var container = await db.Database.CreateContainerIfNotExistsAsync(
                nameof(Campaign), "/id");
            var campaignContainer = new CampaignContainer(container);
            services.AddSingleton(campaignContainer);
        }
    }
}