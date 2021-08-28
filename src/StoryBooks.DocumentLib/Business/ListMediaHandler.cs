using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Azure.Cosmos;
using StoryBooks.DocumentLib.Dto;
using StoryBooks.DocumentLib.Repository;
using StoryBooks.Shared.Cosmos;

namespace StoryBooks.DocumentLib.Business
{
    public class ListMediaHandler : IRequestHandler<ListMediaHandler.ListMediaQuery,
        IEnumerable<MediaDto>>
    {
        private readonly IMediaRepository _mediaRepository;

        public ListMediaHandler(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public async Task<IEnumerable<MediaDto>> Handle(ListMediaQuery request,
            CancellationToken cancellationToken)
        {
            var options = new QueryRequestOptions
            {
                // The partition key is the campaign id
                // We just have to get the whole list of media from the partition
                PartitionKey = new PartitionKey(request.CampaignId)
            };
            var iterator = _mediaRepository.Container.GetItemQueryIterator<Models.Media>(requestOptions: options);
            var models = await iterator.ToListAsync(cancellationToken);
            return models.Select(MediaDto.FromModel);
        }

        public class ListMediaQuery : IRequest<IEnumerable<MediaDto>>
        {
            public string CampaignId { get; }

            public ListMediaQuery(string campaignId)
            {
                CampaignId = campaignId;
            }
        }
    }
}