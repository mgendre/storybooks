using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace StoryBooks.MediaLib.Utils
{
    public static class AzureStorageUtils
    {
        public static async Task UploadBlob(string connectionString, string containerName, string blobName,
            Stream blobContentStream)
        {
            var containerClient = new BlobContainerClient(connectionString, containerName);
            
            await containerClient.CreateIfNotExistsAsync();
            
            await containerClient.UploadBlobAsync(blobName, blobContentStream);
        }
        
        public static async Task DownloadBlob(string connectionString, string containerName, string blobName, Stream stream)
        {
            var containerClient = new BlobContainerClient(connectionString, containerName);
            
            var blobClient = containerClient.GetBlobClient(blobName);
            var response = await blobClient.DownloadToAsync(stream);
            if (response.Status != 200)
            {
                throw new MediaLibException($"Could not download blob {blobName}, " +
                                            $"response status is: {response.Status}");
            }
        }
        
        public static async Task DeleteBlob(string connectionString, string containerName, string blobName)
        {
            var containerClient = new BlobContainerClient(connectionString, containerName);
            
            var response = await containerClient.DeleteBlobAsync(blobName);
            if (response.Status != 200)
            {
                throw new MediaLibException($"Could not delete blob {blobName}, " +
                                            $"response status is: {response.Status}");
            }
        }

        public static async Task DeleteContainer(string connectionString, string containerName)
        {
            var containerClient = new BlobContainerClient(connectionString, containerName);

            var response = await containerClient.DeleteAsync();
            if (response.Status != 200)
            {
                throw new MediaLibException($"Could not delete container {containerName}, " +
                                            $"response status is: {response.Status}");
            }
        }
    }
}
