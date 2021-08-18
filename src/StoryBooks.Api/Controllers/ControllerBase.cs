using System;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using StoryBooks.Api.Dto;
using StoryBooks.Api.Infra.Exceptions;

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
    }
}