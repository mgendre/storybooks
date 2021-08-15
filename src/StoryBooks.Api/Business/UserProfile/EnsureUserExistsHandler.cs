using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StoryBooks.Api.Dto;
using StoryBooks.Api.Repository;

namespace StoryBooks.Api.Business.UserProfile
{
    public class EnsureUserExistsHandler : IRequestHandler<EnsureUserExistsHandler.EnsureUserExistsCommand>
    {

        private readonly ICampaignRepository _repository;

        public EnsureUserExistsHandler(ICampaignRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(EnsureUserExistsCommand request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }

        public class EnsureUserExistsCommand : IRequest
        {
            public CurrentUserDto CurrentUser { get; }
            public EnsureUserExistsCommand(CurrentUserDto currentUser)
            {
                CurrentUser = currentUser;
            }
        }
    }
}