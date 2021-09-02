using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoryBooks.Api.Business.Campaign;
using StoryBooks.Api.Business.Scenario;
using StoryBooks.Api.Dto;
using ControllerBase = StoryBooks.Shared.Controllers.ControllerBase;

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
        
        [HttpGet("{id}")]
        public async Task<CampaignDto> Get(string id)
        {
            await VerifyCurrentUserCampaignAccess(id);
            return await MediatR.Send(new GetCampaignHandler.GetCampaignQuery(id, GetCurrentUser().Email));
        }

        [HttpPost]
        public async Task<CampaignDto> Create(CampaignUpdateDto updateDto)
        {
            var cu = GetCurrentUser();
            var command = new CreateCampaignHandler.CreateCampaignCommand(updateDto, cu.Email);
            var created = await MediatR.Send(command);
            _logger.LogInformation("Campaign {CampaignId} created for user with email {UserEmail}",
                created.Id, cu.Email);

            return CampaignDto.FromModel(created);
        }

        [HttpPut("{id}")]
        public async Task Update(string id, CampaignUpdateDto updateDto)
        {
            await VerifyCurrentUserCampaignAccess(id);
            await MediatR.Send(new UpdateCampaignHandler.UpdateCampaignCommand(id, updateDto));
        }
        
        // Scenarios
        [HttpGet("{campaignId}/scenarios")]
        public async Task<IEnumerable<ScenarioDto>> ListScenarios(string campaignId)
        {
            await VerifyCurrentUserCampaignAccess(campaignId);
            return await MediatR.Send(new ListScenariosHandler.ListScenariosQuery(campaignId));
        }
        
        [HttpPost("{campaignId}/scenarios/")]
        public async Task<ScenarioDto> CreateScenario(string campaignId, ScenarioUpdateDto scenario)
        {
            await VerifyCurrentUserCampaignAccess(campaignId);
            return await MediatR.Send(new SaveScenariosHandler.SaveScenariosCommand(campaignId, null, scenario));
        }

        [HttpPut("{campaignId}/scenarios/{scenarioId}")]
        public async Task<ScenarioDto> UpdateScenario(string campaignId, string scenarioId, ScenarioUpdateDto scenario)
        {
            await VerifyCurrentUserCampaignAccess(campaignId);
            return await MediatR.Send(new SaveScenariosHandler.SaveScenariosCommand(campaignId, scenarioId, scenario));
        }
        
        [HttpDelete("{campaignId}/scenarios/{scenarioId}")]
        public async Task DeleteScenario(string campaignId, string scenarioId)
        {
            await VerifyCurrentUserCampaignAccess(campaignId);
            await MediatR.Send(new DeleteScenariosHandler.DeleteScenariosCommand(campaignId, scenarioId));
        }
    }
}