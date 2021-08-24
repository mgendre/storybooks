using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoryBooks.MediaLib.Service;

namespace StoryBooks.MediaLib
{
    [ApiController]
    public class MediaLibController
    {

        private readonly IMediaLibService _mediaLibService;

        public MediaLibController(IMediaLibService mediaLibService)
        {
            _mediaLibService = mediaLibService;
        }

        [HttpGet("media-lib2")]
        public async Task Media()
        {
            string content = "OK";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            await writer.WriteAsync(content);
            await writer.FlushAsync();
            stream.Position = 0;
            await _mediaLibService.UploadMedia("test", "test.txt", stream);
        }
    }
}