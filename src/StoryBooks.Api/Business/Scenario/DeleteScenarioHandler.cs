using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using StoryBooks.Api.Dto;
using StoryBooks.Api.Repository;

namespace StoryBooks.Api.Business.Scenario
{
    public class DeleteScenariosHandler : IRequestHandler<DeleteScenariosHandler.DeleteScenariosCommand>
    {
        private readonly IScenarioRepository _scenarioRepository;
        private readonly ILogger<SaveScenariosHandler> _logger;

        public DeleteScenariosHandler(
            IScenarioRepository scenarioRepository, ILogger<SaveScenariosHandler> logger)
        {
            _scenarioRepository = scenarioRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteScenariosCommand request, CancellationToken cancellationToken)
        {
            await _scenarioRepository.Delete(request.ScenarioId, 
                new PartitionKey(request.CampaignId), cancellationToken);
            _logger.LogInformation("Scenario {ScenarioId} of campaign {CampaignId}",
                request.ScenarioId, request.CampaignId);
            return Unit.Value;
        }

        public class DeleteScenariosCommand : IRequest<ScenarioDto>, IRequest<Unit>
        {
            public string CampaignId { get; }
            
            public string ScenarioId { get; }
            
            public DeleteScenariosCommand(string campaignId, string scenarioId)
            {
                CampaignId = campaignId;
                ScenarioId = scenarioId;
            }
        }
    }
}