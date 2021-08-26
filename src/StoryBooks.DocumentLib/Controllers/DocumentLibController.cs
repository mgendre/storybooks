using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoryBooks.DocumentLib.Service;

namespace StoryBooks.DocumentLib.Controllers
{
    [Authorize]
    [ApiController]
    public class DocumentLibController
    {

        private readonly IDocumentLibService _documentLibService;

        public DocumentLibController(IDocumentLibService documentLibService)
        {
            _documentLibService = documentLibService;
        }

        [HttpGet("")]
        public async Task Media()
        {
            string content = "OK asdsadasd";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            await writer.WriteAsync(content);
            await writer.FlushAsync();
            stream.Position = 0;
            await _documentLibService.UploadMedia("test", "test2.txt", stream);
        }
    }
}