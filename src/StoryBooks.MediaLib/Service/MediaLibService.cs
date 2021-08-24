using System.IO;
using System.Threading.Tasks;
using StoryBooks.MediaLib.Utils;

namespace StoryBooks.MediaLib.Service
{
    public class MediaLibService : IMediaLibService
    {

        private readonly MediaLibOptions _options;

        public MediaLibService(MediaLibOptions options)
        {
            _options = options;
        }

        public async Task UploadMedia(string containerName, string blobName, Stream contentStream)
        {
            await AzureStorageUtils.UploadBlob(_options.StorageConnectionString, containerName, blobName,
                contentStream);
        }

        public async Task DownloadMedia(string containerName, string blobName, Stream downloadStream)
        {
            await AzureStorageUtils.DownloadBlob(_options.StorageConnectionString, containerName, blobName,
                downloadStream);
        }

        public async Task DeleteMedia(string containerName, string blobName)
        {
            await AzureStorageUtils.DeleteBlob(_options.StorageConnectionString, containerName, blobName);
        }

        public async Task DeleteContainer(string containerName)
        {
            await AzureStorageUtils.DeleteContainer(_options.StorageConnectionString, containerName);
        }
    }
}
