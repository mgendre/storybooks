using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace StoryBooks.Api.Business.Campaign
{
    public class ListCampaignHandler : IRequestHandler<ListCampaignHandler.ListCampaignsCommand,
        IReadOnlyCollection<Models.Campaign>>
    {
        public Task<IReadOnlyCollection<Models.Campaign>> Handle(ListCampaignsCommand request,
            CancellationToken cancellationToken)
        {
            IReadOnlyCollection<Models.Campaign> results = new[]
            {
                new Models.Campaign
                {
                    Id = "asd"
                }
            }.ToList();
            return Task.FromResult(results);
        }

        public class ListCampaignsCommand : IRequest<IReadOnlyCollection<Models.Campaign>>
        {
        }
    }
}