using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoryBooks.Api.Dto;

namespace StoryBooks.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/campaigns")]
    public class ScenarioController
    {
        // Scenarios
        [HttpPut(":campaignId/scenarios/:scenarioId")]
        public Task<ScenarioDto> UpdateScenario(string campaignId, string scenarioId, ScenarioDto scenario)
        {
            throw new NotImplementedException();
        }
    }
}
