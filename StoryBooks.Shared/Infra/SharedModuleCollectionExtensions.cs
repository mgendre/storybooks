using System.Threading.Tasks;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using StoryBooks.Shared.Cosmos;
using StoryBooks.Shared.Repository;

namespace StoryBooks.Shared.Infra
{
    public static class SharedCollectionExtensions
    {
        /// <summary>
        /// It create the database if not exists, use this initializer before other Cosmos initializer
        /// </summary>
        public static IServiceCollection AddSharedModule(this IServiceCollection services, CosmosDbSettings settings)
        {
            Init(services, settings).GetAwaiter().GetResult();
            return services;
        }

        private static async Task Init(IServiceCollection services, CosmosDbSettings settings)
        {
            var client = CosmosUtils.CreateClient(settings);
            services.AddSingleton(client);
            var db = await client.CreateDatabaseIfNotExistsAsync(settings.DatabaseName);
            
            var userProfileContainer = new UserProfileContainer(
                await db.Database.CreateContainerIfNotExistsAsync(
                    nameof(UserProfile), "/PartitionKey")
            );
            
            services.AddSingleton(userProfileContainer);
            services.AddTransient<IUserProfileRepository, UserProfileRepository>();
            
            services.AddMediatR(typeof(SharedCollectionExtensions));
        }
    }
}