using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Azure.Cosmos;
using StoryBooks.Api.Dto;
using StoryBooks.Api.Infra.CosmosDb;
using StoryBooks.Api.Infra.CosmosDb.Containers;

namespace StoryBooks.Api.Business.Campaign
{
    public class ListCampaignHandler : IRequestHandler<ListCampaignHandler.ListCampaignsQuery,
        IEnumerable<CampaignListItemDto>>
    {

        private readonly Container _container;

        public ListCampaignHandler(CampaignContainer campaignContainer)
        {
            _container = campaignContainer.Container;
        }

        public async Task<IEnumerable<CampaignListItemDto>> Handle(ListCampaignsQuery request,
            CancellationToken cancellationToken)
        {
            var queryDefinition = new QueryDefinition("SELECT c.id, c.PartitionKey, c.Name, c.CreationDate, c.ModificationDate FROM " +
                                                      $"{nameof(Models.Campaign)} c");
            var feedIterator = _container.GetItemQueryIterator<Models.Campaign>(queryDefinition, requestOptions: new QueryRequestOptions
            {
                PartitionKey = new PartitionKey("TODO: PASS OWNER")
            });
            
            var campaigns = await feedIterator.ToListAsync(cancellationToken);
            return campaigns.Select(c => new CampaignListItemDto(c));
        }

        public class ListCampaignsQuery : IRequest<IEnumerable<CampaignListItemDto>>
        {
        }
    }
}