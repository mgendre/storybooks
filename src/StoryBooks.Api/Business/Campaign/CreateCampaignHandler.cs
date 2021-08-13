using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StoryBooks.Api.Business.Repository;
using StoryBooks.Api.Dto;
using StoryBooks.Api.Infra.CosmosDb.Containers;
using StoryBooks.Models;

namespace StoryBooks.Api.Business.Campaign
{
    public class CreateCampaignHandler : IRequestHandler<CreateCampaignHandler.CreateCampaignCommand>
    {

        private readonly ICampaignRepository _repository;

        public CreateCampaignHandler(ICampaignRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var toCreate = new Models.Campaign
            {
                PartitionKey = "TODO: PASS OWNER",
                Status = CampaignStatus.InProgress,
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now
            };
            request.ToUpdate.Patch(toCreate);
            await _repository.Create(toCreate, cancellationToken);

            return Unit.Value;
        }

        public class CreateCampaignCommand : IRequest
        {
            public CampaignUpdateDto ToUpdate { get; }

            public CreateCampaignCommand(CampaignUpdateDto toUpdate)
            {
                ToUpdate = toUpdate;
            }
        }
    }
}