using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoryBooks.Api.Business.Campaign;
using StoryBooks.Api.Dto;

namespace StoryBooks.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/campaigns")]
    public class CampaignController : ControllerBase
    {
        private readonly ILogger<CampaignController> _logger;
        public CampaignController(IMediator mediatR, IHttpContextAccessor httpContext, ILogger<CampaignController> logger) :
            base(mediatR, httpContext)
        {
            _logger = logger;
        }

        [HttpGet]
        public Task<IEnumerable<CampaignListItemDto>> ListAll()
        {
            return MediatR.Send(new ListCampaignHandler.ListCampaignsQuery(GetCurrentUser().Email));
        }

        [HttpPost]
        public async Task Create(CampaignUpdateDto updateDto)
        {
            var cu = GetCurrentUser();
            var command = new CreateCampaignHandler.CreateCampaignCommand(updateDto, cu.Email);
            var created = await MediatR.Send(command);
            _logger.LogInformation("Campaign {CampaignId} created for user with email {UserEmail}",
                created.Id, cu.Email);
        }

        [HttpPut(":id/:partitionKey")]
        public Task Update(Guid id, string partitionKey, CampaignUpdateDto updateDto)
        {
            return MediatR.Send(new UpdateCampaignHandler.UpdateCampaignCommand(id, partitionKey, updateDto));
        }
    }
}