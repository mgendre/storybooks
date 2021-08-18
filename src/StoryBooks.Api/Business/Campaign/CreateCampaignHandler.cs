using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Azure.Cosmos;
using StoryBooks.Api.Business.UserProfile;
using StoryBooks.Api.Dto;
using StoryBooks.Api.Repository;
using StoryBooks.Models;

namespace StoryBooks.Api.Business.Campaign
{
    public class CreateCampaignHandler : IRequestHandler<CreateCampaignHandler.CreateCampaignCommand, Models.Campaign>
    {

        private readonly ICampaignRepository _campaignRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public CreateCampaignHandler(
            ICampaignRepository campaignRepository, 
            IUserProfileRepository userProfileRepository)
        {
            _campaignRepository = campaignRepository;
            _userProfileRepository = userProfileRepository;
        }

        public async Task<Models.Campaign> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            // Algo:
            // 1: Get the user profile
            // 2: Create the campaign
            // 3: Link the campaign to the user profile
            // 4; Profit
            
            var userProfile = await _userProfileRepository.GetProfile(request.UserMail, cancellationToken);
            
            var toCreate = new Models.Campaign
            {
                Status = CampaignStatus.InProgress,
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now
            };
            request.ToUpdate.Patch(toCreate);
            
            var created = await _campaignRepository.Create(toCreate, cancellationToken);

            await _userProfileRepository.Update(userProfile.Id, new PartitionKey(userProfile.PartitionKey), profile =>
            {
                profile.CampaignIds.Add(created.Id);
            }, cancellationToken);

            return created;
        }

        public class CreateCampaignCommand : IRequest<Models.Campaign>
        {
            public CampaignUpdateDto ToUpdate { get; }
            
            public string UserMail { get; }

            public CreateCampaignCommand(CampaignUpdateDto toUpdate, string userMail)
            {
                ToUpdate = toUpdate;
                UserMail = userMail;
            }
        }
    }
}