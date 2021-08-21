using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using StoryBooks.Api.Dto;
using StoryBooks.Api.Repository;

namespace StoryBooks.Api.Business.Scenario
{
    public class SaveScenariosHandler : IRequestHandler<SaveScenariosHandler.SaveScenariosCommand, ScenarioDto>
    {
        private readonly IScenarioRepository _scenarioRepository;
        private readonly ILogger<SaveScenariosHandler> _logger;

        public SaveScenariosHandler(
            IScenarioRepository scenarioRepository, ILogger<SaveScenariosHandler> logger)
        {
            _scenarioRepository = scenarioRepository;
            _logger = logger;
        }

        public async Task<ScenarioDto> Handle(SaveScenariosCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.ScenarioId))
            {
                // Creation
                var toCreate = new Models.Scenario
                {
                    CampaignId = request.CampaignId
                };

                request.Scenario.Patch(toCreate);
                var created = await _scenarioRepository.Create(toCreate, cancellationToken);
                _logger.LogInformation("Scenario with id {ScenarioId} for campaign {CampaignId} created",
                    created.Id, request.CampaignId);
                return ScenarioDto.FromModel(created);
            }
            
            // Update
            var updated = await _scenarioRepository.Update(request.ScenarioId, 
                new PartitionKey(request.CampaignId), scenario =>
            {
                request.Scenario.Patch(scenario);
            }, cancellationToken);
            _logger.LogInformation("Scenario with id {ScenarioId} for campaign {CampaignId} updated",
                updated.Id, request.CampaignId);
            return ScenarioDto.FromModel(updated);
        }

        public class SaveScenariosCommand : IRequest<ScenarioDto>
        {
            public string CampaignId { get; }
            
            public string? ScenarioId { get; }
            
            public ScenarioUpdateDto Scenario { get; }

            public SaveScenariosCommand(string campaignId, string? scenarioId, ScenarioUpdateDto scenario)
            {
                CampaignId = campaignId;
                ScenarioId = scenarioId;
                Scenario = scenario;
            }
        }
    }
}