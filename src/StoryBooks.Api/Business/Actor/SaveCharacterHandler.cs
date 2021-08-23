using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using StoryBooks.Api.Business.Scenario;
using StoryBooks.Api.Dto.Actor;
using StoryBooks.Api.Repository;
using StoryBooks.Models;

namespace StoryBooks.Api.Business.Actor
{
    public class SaveCharacterHandler : IRequestHandler<SaveCharacterHandler.SaveCharacterCommand, CharacterDto>
    {
        private readonly IActorRepository<Character> _actorRepository;
        private readonly ILogger<SaveScenariosHandler> _logger;

        public SaveCharacterHandler(
            IActorRepository<Character> actorRepository, ILogger<SaveScenariosHandler> logger)
        {
            _actorRepository = actorRepository;
            _logger = logger;
        }

        public async Task<CharacterDto> Handle(SaveCharacterCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.ActorId))
            {
                // Creation
                var toCreate = new Character
                {
                    CampaignId = request.CampaignId
                };

                request.Actor.Patch(toCreate);
                var created = await _actorRepository.Create(toCreate, cancellationToken);
                _logger.LogInformation("Actor with id {ActorId} and type {ActorType} for campaign {CampaignId} created",
                    created.Id, created.Type, request.CampaignId);
                return CharacterDto.FromModel(created);
            }
            
            // Update
            var updated = await _actorRepository.Update(request.ActorId, 
                new PartitionKey(request.CampaignId), actor =>
            {
                request.Actor.Patch(actor);
            }, cancellationToken);
            _logger.LogInformation("Actor with id {ActorId} and type {ActorType} for campaign {CampaignId} updated",
                updated.Id, updated.Type, request.CampaignId);
            return CharacterDto.FromModel(updated);
        }

        public class SaveCharacterCommand : IRequest<CharacterDto>
        {
            public string CampaignId { get; }
            
            public string? ActorId { get; }
            
            public CharacterUpdateDto Actor { get; }

            public SaveCharacterCommand(string campaignId, string? actorId, CharacterUpdateDto actor)
            {
                CampaignId = campaignId;
                ActorId = actorId;
                Actor = actor;
            }
        }
    }
}