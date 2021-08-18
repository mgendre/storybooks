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
            var userProfile = await _userProfileRepository.GetProfile(request.userMail, cancellationToken);

            var campaigns = new List<CampaignListItemDto>();
            foreach (var campaignId in userProfile.CampaignIds)
            {
                var model = await _campaignRepository.GetById(campaignId, cancellationToken);
                campaigns.Add(new CampaignListItemDto(model));
            }

            return campaigns;
        }

        public class ListCampaignsQuery : IRequest<IEnumerable<CampaignListItemDto>>
        {
            public ListCampaignsQuery(string userMail)
            {
                this.userMail = userMail;
            }

            public string userMail { get; }
        }
    }
}