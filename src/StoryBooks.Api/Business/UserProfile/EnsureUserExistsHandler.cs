using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using StoryBooks.Api.Dto;
using StoryBooks.Api.Repository;

namespace StoryBooks.Api.Business.UserProfile
{
    public class EnsureUserExistsHandler : IRequestHandler<EnsureUserExistsHandler.EnsureUserExistsCommand, UserProfileDto>
    {

        private readonly IUserProfileRepository _repository;
        private readonly IMediator _mediator;
        private readonly ILogger<EnsureUserExistsHandler> _logger;

        public EnsureUserExistsHandler(
            IUserProfileRepository repository, 
            IMediator mediator, 
            ILogger<EnsureUserExistsHandler> logger)
        {
            _repository = repository;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<UserProfileDto> Handle(EnsureUserExistsCommand request, CancellationToken cancellationToken)
        {
            var currentUser = request.CurrentUser;

            var existing = await _mediator.Send(
                new FindUserHandler.FindUserHandlerCommand(currentUser.Email), cancellationToken);

            if (existing != null)
            {
                return existing;
            }

            
            // We should create a new user profile
            
            var profile = new Models.UserProfile
            {
                Id = Guid.NewGuid().ToString(),
                Issuer = currentUser.Issuer,
                SubjectId = currentUser.SubjectId,
                Campaigns = Array.Empty<Models.Campaign>(),
                Email = currentUser.Email,
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName
            };
            var created = await _repository.Create(profile, cancellationToken);
            
            _logger.LogInformation("User profile {UserProfileId} created for email {UserEmail}",
                profile.Id, profile.Email);
            
            return UserProfileDto.FromModel(created);
        }

        public class EnsureUserExistsCommand : IRequest<UserProfileDto>
        {
            public CurrentUserDto CurrentUser { get; }
            public EnsureUserExistsCommand(CurrentUserDto currentUser)
            {
                CurrentUser = currentUser;
            }
        }
    }
}