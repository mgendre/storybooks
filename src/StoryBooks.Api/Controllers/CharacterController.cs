using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoryBooks.Api.Business.Actor;
using StoryBooks.Api.Dto.Actor;

namespace StoryBooks.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/campaigns")]
    public class CharacterController : ControllerBase
    {
        public CharacterController(IMediator mediatR, IHttpContextAccessor httpContext)
            : base(mediatR, httpContext)
        {
        }

        [HttpGet("{campaignId}/characters")]
        public async Task<IEnumerable<CharacterDto>> FindAll(string campaignId)
        {
            await VerifyCurrentUserCampaignAccess(campaignId);
            return await MediatR.Send(new ListCharactersHandler.ListCharactersQuery(campaignId));
        }
        
        [HttpPost("{campaignId}/characters")]
        public async Task<CharacterDto> Create(string campaignId, CharacterUpdateDto updateDto)
        {
            await VerifyCurrentUserCampaignAccess(campaignId);
            return await MediatR.Send(new SaveCharacterHandler.SaveCharacterCommand(campaignId, null, updateDto));
        }
        
        [HttpPut("{campaignId}/characters/{actorId}")]
        public async Task<CharacterDto> Update(string campaignId, string actorId, CharacterUpdateDto updateDto)
        {
            await VerifyCurrentUserCampaignAccess(campaignId);
            return await MediatR.Send(new SaveCharacterHandler.SaveCharacterCommand(campaignId, actorId, updateDto));
        }
        
        [HttpDelete("{campaignId}/characters/{actorId}")]
        public async Task Delete(string campaignId, string actorId)
        {
            await VerifyCurrentUserCampaignAccess(campaignId);
            await MediatR.Send(new DeleteCharacterHandler.DeleteCharacterCommand(campaignId, actorId));
        }
    }
}
