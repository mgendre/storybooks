using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using StoryBooks.DocumentLib.Infra;
using StoryBooks.DocumentLib.Utils;

namespace StoryBooks.DocumentLib.Service
{
    public class DocumentLibService : IDocumentLibService
    {

        private readonly DocumentLibOptions _options;

        public DocumentLibService(IOptions<DocumentLibOptions> options)
        {
            _options = options.Value;
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
