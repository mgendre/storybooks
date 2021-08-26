using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StoryBooks.Shared.Cosmos;
using StoryBooks.Shared.Repository;

namespace StoryBooks.Shared.Infra
{
    public static class SharedCollectionExtensions
    {
        public static IServiceCollection AddSharedModule(this IServiceCollection services, CosmosDbSettings settings)
        {
            Init(services, settings).GetAwaiter().GetResult();
            return services;
        }

        private static async Task Init(IServiceCollection services, CosmosDbSettings settings)
        {
            var db = await CosmosUtils.CreateDataBaseIfNotExists(settings);
            var userProfileContainer = new UserProfileContainer(
                await db.Database.CreateContainerIfNotExistsAsync(
                    nameof(UserProfile), "/PartitionKey")
            );
            services.AddSingleton(userProfileContainer);
            services.AddSingleton<IUserProfileRepository, UserProfileRepository>();
            
            services.AddMediatR(typeof(SharedCollectionExtensions));
        }
    }
}