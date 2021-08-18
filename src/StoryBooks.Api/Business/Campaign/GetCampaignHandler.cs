using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StoryBooks.Api.Dto;
using StoryBooks.Api.Repository;

namespace StoryBooks.Api.Business.Campaign
{
    public class GetCampaignHandler : IRequestHandler<GetCampaignHandler.GetCampaignQuery, CampaignDto>
    {

        private readonly ICampaignRepository _campaignRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public GetCampaignHandler(
            ICampaignRepository campaignRepository, 
            IUserProfileRepository userProfileRepository)
        {
            _campaignRepository = campaignRepository;
            _userProfileRepository = userProfileRepository;
        }

        public async Task<CampaignDto> Handle(GetCampaignQuery request, CancellationToken cancellationToken)
        {
            var userProfile = await _userProfileRepository.GetProfile(request.UserMail, cancellationToken);
            
            if (!userProfile.CampaignIds.Contains(request.CampaignId))
            {
                throw new UnauthorizedAccessException($"Campaign with id {request.CampaignId} " +
                                                      $"is not linked to user {request.UserMail}");
            }

            var campaign = await _campaignRepository.GetById(request.CampaignId, cancellationToken);
            return CampaignDto.FromModel(campaign);
        }

        public class GetCampaignQuery : IRequest<CampaignDto>
        {
            public GetCampaignQuery(string campaignId, string userMail)
            {
                UserMail = userMail;
                CampaignId = campaignId;
            }

            public string CampaignId { get; }
            public string UserMail { get; }
            
        }
    }
}