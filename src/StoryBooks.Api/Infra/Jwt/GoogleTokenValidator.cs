using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace StoryBooks.Api.Infra.Jwt
{
    public class GoogleTokenValidator : ISecurityTokenValidator {
        private readonly string _clientId;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public GoogleTokenValidator(string clientId)
        {
            _clientId = clientId;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public bool CanValidateToken => true;

        public int MaximumTokenSizeInBytes { get; set; } = TokenValidationParameters.DefaultMaximumTokenSizeInBytes;

        public bool CanReadToken(string securityToken)
        {
            return _tokenHandler.CanReadToken(securityToken);
        }

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            validatedToken = null;
            var payload = GoogleJsonWebSignature.ValidateAsync(securityToken, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience =  new[] { _clientId }
            }).Result; // here is where I delegate to Google to validate
        
            var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, payload.Name),
                    new(ClaimTypes.Name, payload.Name),
                    new(JwtRegisteredClaimNames.FamilyName, payload.FamilyName),
                    new(JwtRegisteredClaimNames.GivenName, payload.GivenName),
                    new(JwtRegisteredClaimNames.Email, payload.Email),
                    new(JwtRegisteredClaimNames.Sub, payload.Subject),
                    new(JwtRegisteredClaimNames.Iss, payload.Issuer),
                };

            var principal = new ClaimsPrincipal();
            principal.AddIdentity(new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme));
            return principal;
        }
    }
}