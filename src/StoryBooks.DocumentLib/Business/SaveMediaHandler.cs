using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using StoryBooks.DocumentLib.Dto;
using StoryBooks.DocumentLib.Repository;
using StoryBooks.DocumentLib.Service;
using StoryBooks.Models;

namespace StoryBooks.DocumentLib.Business
{
    public class SaveMediaHandler : IRequestHandler<SaveMediaHandler.SaveMediaCommand, MediaDto>
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly ILogger<SaveMediaHandler> _logger;
        private readonly IDocumentLibService _documentLibService;

        public SaveMediaHandler(
            IMediaRepository mediaRepository,
            ILogger<SaveMediaHandler> logger, IDocumentLibService documentLibService)
        {
            _mediaRepository = mediaRepository;
            _logger = logger;
            _documentLibService = documentLibService;
        }

        public async Task<MediaDto> Handle(SaveMediaCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.MediaId))
            {
                // Creation
                var toCreate = new Media
                {
                    CampaignId = request.CampaignId,
                    CreationDate = DateTime.Now,
                    ModificationDate = DateTime.Now,
                    ExternalUri = request.ExternalUri,
                    StorageType = MediaStorageType.ExternalUri
                };

                if (request.File != null)
                {
                    var createdDocument = await CreateDocument(toCreate, request.File!);
                    toCreate.Document = createdDocument;
                    toCreate.StorageType = MediaStorageType.Document;
                }

                await _mediaRepository.Create(toCreate, cancellationToken);

                return MediaDto.FromModel(toCreate);
            }

            // Modification
            var existing = await _mediaRepository.GetById(
                request.MediaId, new PartitionKey(request.CampaignId), cancellationToken);

            existing.ModificationDate = DateTime.Now;

            if (existing.StorageType == MediaStorageType.Document)
            {

                if (request.File == null)
                {
                    throw new InvalidOperationException("No file uploaded");
                }

                // This will mutate the document inside media object
                await UpdateDocument(existing, request.File!);
            }

            await _mediaRepository.Update(request.MediaId, new PartitionKey(request.CampaignId),
                media =>
                {
                    media.ExternalUri = request.ExternalUri;
                }, 
                cancellationToken);

            return MediaDto.FromModel(existing);
        }

        private async Task<Document> CreateDocument(Media media, IFormFile file)
        {
            var doc = new Document
            {
                Filename = file.FileName,
            };

            await using var stream = file.OpenReadStream();
            await _documentLibService.UploadMedia(media.CampaignId, doc.Id, stream);

            _logger.LogInformation("New document with id {DocumentId} created in container {DocumentContainerId}",
                doc.Id, media.CampaignId);

            return doc;
        }

        private async Task<Document> UpdateDocument(Media media, IFormFile file)
        {
            if (media.Document == null)
            {
                throw new InvalidOperationException("Media document should not be null");
            }

            media.Document.Filename = file.FileName;

            await using var stream = file.OpenReadStream();
            await _documentLibService.UploadMedia(media.CampaignId, media.Document.Id, stream);

            _logger.LogInformation("Document with id {DocumentId} updated in container {DocumentContainerId}",
                media.Document.Id, media.CampaignId);

            return media.Document;
        }

        public class SaveMediaCommand : IRequest<MediaDto>
        {
            public string CampaignId { get; }

            public string? MediaId { get; }
            
            public string? Label { get; }
            
            public Uri? ExternalUri { get; }
            
            public IFormFile? File { get; }

            public SaveMediaCommand(
                string campaignId, 
                string? mediaId, 
                IFormFile? file, 
                Uri? externalUri = null, 
                string? label = null)
            {
                CampaignId = campaignId;
                MediaId = mediaId;
                File = file;
                ExternalUri = externalUri;
                Label = label;
            }
        }
    }
}