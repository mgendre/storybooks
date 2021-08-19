using System;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using StoryBooks.Api.Business.UserProfile;
using StoryBooks.Api.Dto;

namespace StoryBooks.Api.Controllers
{
    public abstract class ControllerBase
    {
        protected readonly IMediator MediatR;
        protected readonly IHttpContextAccessor HttpContext;

        protected ControllerBase(IMediator mediatR, IHttpContextAccessor httpContext)
        {
            MediatR = mediatR;
            HttpContext = httpContext;
        }

        protected CurrentUserDto? FindCurrentUser()
        {
            var identity = HttpContext?.HttpContext?.User;
            if (identity == null)
            {
                return null;
            }

            var email = identity.FindFirst(c => c.Type == JwtRegisteredClaimNames.Email);

            if (email == null)
            {
                throw new InvalidOperationException("Current user has no email");
            }

            return new CurrentUserDto(
                issuer: identity.FindFirstValue("authentication_provider") ?? "",
                subjectId: identity.FindFirstValue(JwtRegisteredClaimNames.Sub) ?? "",
                email: email.Value,
                firstName: identity.FindFirstValue(JwtRegisteredClaimNames.GivenName) ?? "",
                lastName: identity.FindFirstValue(JwtRegisteredClaimNames.FamilyName) ?? ""
            );
        }

        protected CurrentUserDto GetCurrentUser()
        {
            return FindCurrentUser() ?? throw new AuthenticationException("No current user found");
        }
        
        protected async Task<UserProfileDto> GetCurrentUserProfile()
        {
            var cu = GetCurrentUser();
            return await MediatR.Send(new FindUserHandler.FindUserHandlerCommand(cu.Email)) ?? 
                   throw new AuthenticationException($"No profile found for user with email {cu.Email}");
        }
    }
}