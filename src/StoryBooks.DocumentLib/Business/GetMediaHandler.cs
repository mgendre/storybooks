using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Azure.Cosmos;
using StoryBooks.DocumentLib.Dto;
using StoryBooks.DocumentLib.Repository;

namespace StoryBooks.DocumentLib.Business
{
    public class GetMediaHandler : IRequestHandler<GetMediaHandler.GetMediaQuery, MediaDto>
    {

        private readonly IMediaRepository _mediaRepository;

        public GetMediaHandler(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public async Task<MediaDto> Handle(GetMediaQuery request, CancellationToken cancellationToken)
        {
            var campaign = await _mediaRepository.GetById(
                request.MediaId, new PartitionKey(request.CampaignId), cancellationToken);
            return MediaDto.FromModel(campaign);
        }

        public class GetMediaQuery : IRequest<MediaDto>
        {
            public GetMediaQuery(string campaignId, string mediaId)
            {
                MediaId = mediaId;
                CampaignId = campaignId;
            }

            public string CampaignId { get; }
            public string MediaId { get; }
            
        }
    }
}