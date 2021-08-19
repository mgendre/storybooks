using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Azure.Cosmos;
using StoryBooks.Api.Dto;
using StoryBooks.Api.Repository;

namespace StoryBooks.Api.Business.Campaign
{
    public class UpdateCampaignHandler : IRequestHandler<UpdateCampaignHandler.UpdateCampaignCommand>
    {

        private readonly ICampaignRepository _repository;

        public UpdateCampaignHandler(ICampaignRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            await _repository.Update(request.Id, new PartitionKey(request.Id.ToString()), 
                campaign => request.ToUpdate.Patch(campaign), cancellationToken);
            return Unit.Value;
        }

        public class UpdateCampaignCommand : IRequest
        {
            public CampaignUpdateDto ToUpdate { get; }
            public string Id { get; }

            public UpdateCampaignCommand(string id, CampaignUpdateDto toUpdate)
            {
                ToUpdate = toUpdate;
                Id = id;
            }
        }
    }
}