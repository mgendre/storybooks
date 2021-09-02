using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoryBooks.Infra.Controllers.Dto;
using StoryBooks.Shared.UserProfile;
using ControllerBase = StoryBooks.Shared.Controllers.ControllerBase;

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

        [HttpGet]
        [Route("current")]
        public async Task<UserProfileDto> EnsureCreated()
        {
            var user = FindCurrentUser();
            if (user == null)
            {
                throw new UnauthorizedAccessException("Current user should not be null");
            }
            return await MediatR.Send(new EnsureUserExistsHandler.EnsureUserExistsCommand(user));
        }
    }
}