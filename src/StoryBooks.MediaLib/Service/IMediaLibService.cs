using System.IO;
using System.Threading.Tasks;

namespace StoryBooks.MediaLib.Service
{
    public interface IMediaLibService
    {
        
        public Task UploadMedia(string containerName, string blobName, Stream contentStream);
        
        public Task DownloadMedia(string containerName, string blobName, Stream downloadStream);
        
        public Task DeleteMedia(string containerName, string blobName);
        
        public Task DeleteContainer(string containerName);
        
    }
}