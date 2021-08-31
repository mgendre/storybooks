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

            services.AddTransient<IDocumentLibService, DocumentLibService>();

            var db = CosmosUtils.CreateClient(settings).GetDatabase(settings.DatabaseName);
            var mediaContainer = new MediaContainer(
                await db.CreateContainerIfNotExistsAsync(
                    nameof(Media), "/PartitionKey")
            );
            services.AddSingleton(mediaContainer);
            services.AddTransient<IMediaRepository, MediaRepository>();

            services.AddMediatR(typeof(DocumentLibServiceCollectionExtensions));
        }
    }
}