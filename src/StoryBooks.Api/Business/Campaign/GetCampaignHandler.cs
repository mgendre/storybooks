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

        public GetCampaignHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<CampaignDto> Handle(GetCampaignQuery request, CancellationToken cancellationToken)
        {
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