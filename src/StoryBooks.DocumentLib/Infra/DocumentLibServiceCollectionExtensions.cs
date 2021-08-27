using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoryBooks.DocumentLib.Repository;
using StoryBooks.DocumentLib.Service;
using StoryBooks.Models;
using StoryBooks.Shared.Cosmos;

namespace StoryBooks.DocumentLib.Infra
{
    public static class DocumentLibServiceCollectionExtensions
    {
        public static IServiceCollection AddMediaLib(
            this IServiceCollection services,
            IConfiguration mediaLibConfigurationSection,
            CosmosDbSettings settings)
        {
            AddMediaLibAsync(services, mediaLibConfigurationSection, settings).GetAwaiter().GetResult();
            return services;
        }

        private static async Task AddMediaLibAsync(IServiceCollection services,
            IConfiguration mediaLibConfigurationSection,
            CosmosDbSettings settings)
        {
            services.Configure<DocumentLibOptions>(mediaLibConfigurationSection);

            services.AddSingleton<IDocumentLibService, DocumentLibService>();

            var db = await CosmosUtils.CreateDataBaseIfNotExists(settings);
            var mediaContainer = new MediaContainer(
                await db.Database.CreateContainerIfNotExistsAsync(
                    nameof(Media), "/PartitionKey")
            );
            services.AddSingleton(mediaContainer);
            services.AddSingleton<IMediaRepository, MediaRepository>();

            services.AddMediatR(typeof(DocumentLibServiceCollectionExtensions));
        }
    }
}