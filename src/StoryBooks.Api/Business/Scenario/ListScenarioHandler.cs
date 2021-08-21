using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Azure.Cosmos;
using StoryBooks.Api.Dto;
using StoryBooks.Api.Infra.CosmosDb;
using StoryBooks.Api.Repository;

namespace StoryBooks.Api.Business.Scenario
{
    public class ListScenariosHandler : IRequestHandler<ListScenariosHandler.ListScenariosQuery,
        IEnumerable<ScenarioDto>>
    {
        private readonly IScenarioRepository _scenarioRepository;

        public ListScenariosHandler(
            IScenarioRepository scenarioRepository
        )
        {
            _scenarioRepository = scenarioRepository;
        }

        public async Task<IEnumerable<ScenarioDto>> Handle(ListScenariosQuery request,
            CancellationToken cancellationToken)
        {
            var options = new QueryRequestOptions
            {
                // The partition key is the campaign id
                // We just have to get the whole list of scenarios from the partition
                PartitionKey = new PartitionKey(request.CampaignId)
            };
            var iterator = _scenarioRepository.Container.GetItemQueryIterator<Models.Scenario>(requestOptions: options);
            var models = await iterator.ToListAsync(cancellationToken);
            return models.Select(ScenarioDto.FromModel);
        }

        public class ListScenariosQuery : IRequest<IEnumerable<ScenarioDto>>
        {
            public string CampaignId { get; }

            public ListScenariosQuery(string campaignId)
            {
                CampaignId = campaignId;
            }
        }
    }
}