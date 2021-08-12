using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Azure.Cosmos;
using StoryBooks.Api.Dto;
using StoryBooks.Api.Infra.CosmosDb.Containers;

namespace StoryBooks.Api.Business.Campaign
{
    public class UpdateCampaignHandler : IRequestHandler<UpdateCampaignHandler.UpdateCampaignCommand>
    {

        private readonly Container _container;

        public UpdateCampaignHandler(CampaignContainer campaignContainer)
        {
            _container = campaignContainer.Container;
        }

        public async Task<Unit> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            var key = new PartitionKey("TODO: PASS OWNER");
            var existing = await _container.ReadItemAsync<Models.Campaign>(request.Id.ToString(),
                key, cancellationToken: cancellationToken);
            request.ToUpdate.Patch(existing.Resource);
            await _container.UpsertItemAsync(existing.Resource, key, cancellationToken: cancellationToken);
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