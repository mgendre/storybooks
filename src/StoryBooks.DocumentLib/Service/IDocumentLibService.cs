using System.IO;
using System.Threading.Tasks;

namespace StoryBooks.DocumentLib.Service
{
    public interface IDocumentLibService
    {
        
        public Task UploadMedia(string containerName, string blobName, Stream contentStream);
        
        public Task DownloadMedia(string containerName, string blobName, Stream downloadStream);
        
        public Task DeleteMedia(string containerName, string blobName);
        
        public Task DeleteContainer(string containerName);
        
    }
}