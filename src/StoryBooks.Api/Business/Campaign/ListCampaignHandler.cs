using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Azure.Cosmos;
using StoryBooks.Api.Dto;
using StoryBooks.Api.Infra.CosmosDb;
using StoryBooks.Api.Infra.CosmosDb.Containers;
using StoryBooks.Api.Repository;

namespace StoryBooks.Api.Business.Campaign
{
    public class ListCampaignHandler : IRequestHandler<ListCampaignHandler.ListCampaignsQuery,
        IEnumerable<CampaignListItemDto>>
    {

        private readonly ICampaignRepository _campaignRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public ListCampaignHandler(
            ICampaignRepository campaignRepository, 
            IUserProfileRepository userProfileRepository)
        {
            _campaignRepository = campaignRepository;
            _userProfileRepository = userProfileRepository;
        }

        public async Task<IEnumerable<CampaignListItemDto>> Handle(ListCampaignsQuery request,
            CancellationToken cancellationToken)
        {
            var userProfile = await _userProfileRepository.GetProfile(request.UserMail, cancellationToken);

            var campaigns = new List<CampaignListItemDto>();

            string query = "select c.id, c.Name, c.Status, c.CreationDate, c.ModificationDate from " +
                           $"{nameof(Models.Campaign)} c";
            foreach (var campaignId in userProfile.CampaignIds)
            {
                var options = new QueryRequestOptions
                {
                    PartitionKey = new PartitionKey(campaignId),
                    MaxItemCount = 1
                };
                var iterator = _campaignRepository.Container.GetItemQueryIterator<Models.Campaign>(query, requestOptions: options);
                var model = await iterator.FirstAsync(cancellationToken);
                campaigns.Add(new CampaignListItemDto(model));
            }

            return campaigns;
        }

        public class ListCampaignsQuery : IRequest<IEnumerable<CampaignListItemDto>>
        {
            public ListCampaignsQuery(string userMail)
            {
                this.UserMail = userMail;
            }

            public string UserMail { get; }
        }
    }
}