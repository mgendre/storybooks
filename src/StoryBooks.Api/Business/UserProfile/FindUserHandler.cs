using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Azure.Cosmos;
using StoryBooks.Api.Dto;
using StoryBooks.Api.Infra.CosmosDb;
using StoryBooks.Api.Repository;

namespace StoryBooks.Api.Business.UserProfile
{
    public class FindUserHandler : IRequestHandler<FindUserHandler.FindUserHandlerCommand, UserProfileDto?>
    {

        private readonly IUserProfileRepository _repository;

        public FindUserHandler(IUserProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserProfileDto?> Handle(FindUserHandlerCommand request, CancellationToken cancellationToken)
        {
            // Partition is by email, we search the first element from the partition
            var options = new QueryRequestOptions
            {
                PartitionKey = new PartitionKey(request.Email)
            };
            var it = _repository.Container.GetItemQueryIterator<Models.UserProfile>(requestOptions: options);
            var collection = await it.ToListAsync(cancellationToken);
            var profile = collection.FirstOrDefault();

            return profile == null ? null : UserProfileDto.FromModel(profile);
        }

        public class FindUserHandlerCommand : IRequest<UserProfileDto?>
        {
            public string Email { get; }
            
            public FindUserHandlerCommand(string  email)
            {
                Email = email;
            }
        }
    }
}