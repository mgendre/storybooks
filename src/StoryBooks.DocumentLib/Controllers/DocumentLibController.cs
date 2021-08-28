using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoryBooks.DocumentLib.Business;
using StoryBooks.DocumentLib.Dto;
using StoryBooks.DocumentLib.Service;
using ControllerBase = StoryBooks.Api.Controllers.ControllerBase;

namespace StoryBooks.DocumentLib.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/campaigns")]
    public class DocumentLibController : ControllerBase
    {

        private readonly IDocumentLibService _documentLibService;

        public DocumentLibController(
            IDocumentLibService documentLibService, 
            IMediator mediatR, 
            IHttpContextAccessor httpContext) : base(mediatR, httpContext)
        {
            _documentLibService = documentLibService;
        }

        [HttpPost("{campaignId}/media/upload")]
        public async Task<MediaDto> UploadAndCreate(string campaignId, IEnumerable<IFormFile> files, string? label)
        {
            await VerifyCurrentUserCampaignAccess(campaignId);
            
            return await MediatR.Send(new SaveMediaHandler.SaveMediaCommand(campaignId, null, files.First(), null, label)); 
        }
        
        [HttpPost("{campaignId}/media/{mediaId}/upload")]
        public async Task<MediaDto> UploadAndReplace(string campaignId, string mediaId, IEnumerable<IFormFile> files, string? label)
        {
            await VerifyCurrentUserCampaignAccess(campaignId);
            
            return await MediatR.Send(new SaveMediaHandler.SaveMediaCommand(campaignId, mediaId, files.First(), null, label)); 
        }
        
        [HttpPost("{campaignId}/media")]
        public async Task<MediaDto> Create(string campaignId, Uri externalUri, string? label)
        {
            await VerifyCurrentUserCampaignAccess(campaignId);
            
            return await MediatR.Send(new SaveMediaHandler.SaveMediaCommand(campaignId, null, null, externalUri, label));
        }
        
        [HttpPut("{campaignId}/media/{mediaId}")]
        public async Task<MediaDto> Update(string campaignId, string mediaId, Uri externalUri, string? label)
        {
            await VerifyCurrentUserCampaignAccess(campaignId);

            return await MediatR.Send(new SaveMediaHandler.SaveMediaCommand(campaignId, mediaId, null, externalUri, label));
        }

        [HttpGet("{campaignId}/media")]
        public async Task<IEnumerable<MediaDto>> List(string campaignId)
        {
            await VerifyCurrentUserCampaignAccess(campaignId);
            
            return await MediatR.Send(new ListMediaHandler.ListMediaQuery(campaignId));
        }

        [HttpGet("asdsad")]
        public async Task Media()
        {
            await VerifyCurrentUserCampaignAccess("ef0fd7b1-3b2a-4121-9e62-8465a129bb51");
            
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