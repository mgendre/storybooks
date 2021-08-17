using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoryBooks.Api.Business.UserProfile;
using StoryBooks.Api.Dto;

namespace StoryBooks.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user-profiles")]
    public class UserProfileController : ControllerBase
    {

        public UserProfileController(IMediator mediatR, IHttpContextAccessor httpContext) : base(mediatR, httpContext)
        {
        }

        [HttpPost]
        [Route("create")]
        public Task EnsureCreated()
        {
            var user = GetCurrentUser();
            if (user == null)
            {
                throw new UnauthorizedAccessException("Current user should not be null");
            }

            return MediatR.Send(new EnsureUserExistsHandler.EnsureUserExistsCommand(user));
        }

        [HttpGet]
        [Route("current")]
        public UserProfileDto GetProfile()
        {
            return GetCurrentUser() ?? throw new UnauthorizedAccessException("User is not signed in");
        }
    }
}