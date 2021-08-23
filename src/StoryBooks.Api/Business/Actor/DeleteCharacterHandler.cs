using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using StoryBooks.Api.Business.Scenario;
using StoryBooks.Api.Repository;
using StoryBooks.Models;

namespace StoryBooks.Api.Business.Actor
{
    public class DeleteCharacterHandler : IRequestHandler<DeleteCharacterHandler.DeleteCharacterCommand>
    {
        private readonly IActorRepository<Character> _actorRepository;
        private readonly ILogger<SaveScenariosHandler> _logger;

        public DeleteCharacterHandler(
            IActorRepository<Character> actorRepository, ILogger<SaveScenariosHandler> logger)
        {
            _actorRepository = actorRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
        {
            await _actorRepository.Delete(request.ActorId, new PartitionKey(request.CampaignId), cancellationToken);
            _logger.LogInformation("Actor with id {ActorId} and type {ActorType} for campaign {CampaignId} deleted",
                             request.ActorId, nameof(Character), request.CampaignId);
            return Unit.Value;
        }

        public class DeleteCharacterCommand : IRequest
        {
            public string CampaignId { get; }
            
            public string ActorId { get; }
            
            public DeleteCharacterCommand(string campaignId, string actorId)
            {
                CampaignId = campaignId;
                ActorId = actorId;
            }
        }
    }
}