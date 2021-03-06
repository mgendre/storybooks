using System;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using StoryBooks.Infra.Controllers.Dto;
using StoryBooks.Shared.UserProfile;

namespace StoryBooks.Shared.Controllers
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
                issuer: identity.FindFirstValue("authentication_provider") ?? string.Empty,
                subjectId: identity.FindFirstValue(JwtRegisteredClaimNames.Sub) ?? string.Empty,
                email: email.Value,
                firstName: identity.FindFirstValue(JwtRegisteredClaimNames.GivenName) ?? string.Empty,
                lastName: identity.FindFirstValue(JwtRegisteredClaimNames.FamilyName) ?? string.Empty
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
        
        protected async Task VerifyCurrentUserCampaignAccess(string campaignId)
        {
            var profile = await GetCurrentUserProfile();
            if (!profile.CampaignIds.Contains(campaignId))
            {
                throw new UnauthorizedAccessException($"User {profile.Email} has no access to campaign {campaignId}");
            }
        }
    }
}