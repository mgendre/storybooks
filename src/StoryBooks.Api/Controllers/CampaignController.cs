using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using StoryBooks.Api.Business.Campaign;
using StoryBooks.Api.Dto;

namespace StoryBooks.Api.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("campaigns")]
    public class CampaignController
    {

        private readonly IMediator _mediatR;

        public CampaignController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpGet]
        public Task<IEnumerable<CampaignListItemDto>> ListAll()
        {
            return _mediatR.Send(new ListCampaignHandler.ListCampaignsQuery());
        }
        
        [HttpPost]
        public Task Create(CampaignUpdateDto updateDto)
        {
            return _mediatR.Send(new CreateCampaignHandler.CreateCampaignCommand(updateDto));
        }
        
        [HttpPut(":id/:partitionKey")]
        public Task Update(Guid id, string partitionKey, CampaignUpdateDto updateDto)
        {
            return _mediatR.Send(new UpdateCampaignHandler.UpdateCampaignCommand(id, partitionKey, updateDto));
        }
    }
}