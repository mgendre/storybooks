using System;
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
            await _repository.Update(request.Id.ToString(), new PartitionKey(request.Id.ToString()), 
                campaign => request.ToUpdate.Patch(campaign), cancellationToken);
            return Unit.Value;
        }

        public class UpdateCampaignCommand : IRequest
        {
            public CampaignUpdateDto ToUpdate { get; }
            public Guid Id { get; }

            public UpdateCampaignCommand(Guid id, CampaignUpdateDto toUpdate)
            {
                ToUpdate = toUpdate;
                Id = id;
            }
        }
    }
}