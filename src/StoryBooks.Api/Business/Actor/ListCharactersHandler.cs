using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StoryBooks.Api.Dto.Actor;
using StoryBooks.Api.Repository;
using StoryBooks.Models;

namespace StoryBooks.Api.Business.Actor
{
    public class ListCharactersHandler : IRequestHandler<ListCharactersHandler.ListCharactersQuery, IEnumerable<CharacterDto>>
    {
        private readonly IActorRepository<Character> _actorRepository;

        public ListCharactersHandler(IActorRepository<Character> actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public async Task<IEnumerable<CharacterDto>> Handle(ListCharactersQuery request, CancellationToken cancellationToken)
        {
            var models = await _actorRepository.FindAll(request.CampaignId, cancellationToken);
            return models.Select(CharacterDto.FromModel);
        }

        public class ListCharactersQuery : IRequest<IEnumerable<CharacterDto>>
        {
            public string CampaignId { get; }
            
            public ListCharactersQuery(string campaignId)
            {
                CampaignId = campaignId;
            }
        }
    }
}