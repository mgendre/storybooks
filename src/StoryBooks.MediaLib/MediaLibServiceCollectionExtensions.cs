using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoryBooks.MediaLib.Service;

namespace StoryBooks.MediaLib
{
    public static class MediaLibServiceCollectionExtensions
    {
        public static IServiceCollection AddMediaLib(this IServiceCollection services,
            IConfiguration mediaLibConfigurationSection)
        {
            services.Configure<MediaLibOptions>(mediaLibConfigurationSection);
            
            services.AddSingleton<IMediaLibService>(new MediaLibService(
                mediaLibConfigurationSection.Get<MediaLibOptions>()));

            return services;
        }
    }
}