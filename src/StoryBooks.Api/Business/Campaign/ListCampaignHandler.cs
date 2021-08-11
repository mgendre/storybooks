using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Azure.Cosmos;
using StoryBooks.Api.Infra;

namespace StoryBooks.Api.Business.Campaign
{
    public class ListCampaignHandler : IRequestHandler<ListCampaignHandler.ListCampaignsCommand,
        IEnumerable<Models.Campaign>>
    {

        private readonly Container _container;

        public ListCampaignHandler(CampaignContainer container)
        {
            _container = container.Container;
        }

        public async Task<IEnumerable<Models.Campaign>> Handle(ListCampaignsCommand request,
            CancellationToken cancellationToken)
        {
            var iterator = await _container.GetItemQueryIterator<Models.Campaign>()
                .ReadNextAsync(cancellationToken);
            return iterator;
        }

        public class ListCampaignsCommand : IRequest<IEnumerable<Models.Campaign>>
        {
        }
    }
}