using System;
using System.Linq;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
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

        protected UserProfileDto? GetCurrentUser()
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

            return new UserProfileDto(
                issuer: identity.FindFirstValue("authentication_provider") ?? "",
                subject: identity.FindFirstValue(JwtRegisteredClaimNames.Sub) ?? "",
                email: email.Value,
                firstName: identity.FindFirstValue(JwtRegisteredClaimNames.GivenName) ?? "",
                lastName: identity.FindFirstValue(JwtRegisteredClaimNames.FamilyName) ?? ""
            );
        }
    }
}