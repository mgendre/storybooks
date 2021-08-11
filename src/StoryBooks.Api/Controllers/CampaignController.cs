using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoryBooks.Api.Business.Campaign;
using StoryBooks.Models;

namespace StoryBooks.Api.Controllers
{
    [ApiController]
    [Route("campaigns")]
    public class CampaignController
    {

        private readonly IMediator _mediatR;

        public CampaignController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpGet]
        public Task<IReadOnlyCollection<Campaign>> ListAll()
        {
            return _mediatR.Send(new ListCampaignHandler.ListCampaignsCommand());
        }
    }
}