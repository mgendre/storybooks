using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using StoryBooks.DocumentLib.Business;
using StoryBooks.DocumentLib.Dto;
using StoryBooks.DocumentLib.Service;
using ControllerBase = StoryBooks.Shared.Controllers.ControllerBase;

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
        
        [AllowAnonymous] // TODO: Change how it's done in angular to pass the token
        [HttpGet("{campaignId}/media/{mediaId}/download")]
        public async Task<FileStreamResult> Download(string campaignId, string mediaId)
        {
            // TODO: Change how it's done in angular to pass the token
            //await VerifyCurrentUserCampaignAccess(campaignId);
            
            var media = await MediatR.Send(new GetMediaHandler.GetMediaQuery(campaignId, mediaId));

            var stream = new MemoryStream();
            await _documentLibService.DownloadMedia(media.CampaignId, media.DocumentId ?? "", stream);
            stream.Flush();
            stream.Position = 0;

            var contentType = media.ContentType ?? "image/jpeg";
            return new FileStreamResult(stream, new MediaTypeHeaderValue(contentType))
            {
                FileDownloadName = media.Filename
            };
        }
    }
}
