using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoryBooks.Api.Business.Campaign;
using StoryBooks.Api.Dto;

namespace StoryBooks.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/campaigns")]
    public class CampaignController : ControllerBase
    {
        public CampaignController(IMediator mediatR, IHttpContextAccessor httpContext) :
            base(mediatR, httpContext)
        {
        }

        [HttpGet]
        public Task<IEnumerable<CampaignListItemDto>> ListAll()
        {
            return MediatR.Send(new ListCampaignHandler.ListCampaignsQuery());
        }

        [HttpPost]
        public Task Create(CampaignUpdateDto updateDto)
        {
            return MediatR.Send(new CreateCampaignHandler.CreateCampaignCommand(updateDto));
        }

        [HttpPut(":id/:partitionKey")]
        public Task Update(Guid id, string partitionKey, CampaignUpdateDto updateDto)
        {
            return MediatR.Send(new UpdateCampaignHandler.UpdateCampaignCommand(id, partitionKey, updateDto));
        }
    }
}