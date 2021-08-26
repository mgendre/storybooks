using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoryBooks.DocumentLib.Service;

namespace StoryBooks.DocumentLib.Infra
{
    public static class DocumentLibServiceCollectionExtensions
    {
        public static IServiceCollection AddMediaLib(this IServiceCollection services,
            IConfiguration mediaLibConfigurationSection)
        {
            services.Configure<DocumentLibOptions>(mediaLibConfigurationSection);

            services.AddSingleton<IDocumentLibService, DocumentLibService>();

            services.AddMediatR(typeof(IServiceCollection));
            
            return services;
        }
    }
}