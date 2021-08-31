using StoryBooks.Models;

namespace StoryBooks.Api.Dto.Actor
{
    public class CharacterDto : AbstractActorDto
    {
        public static CharacterDto FromModel(Character model)
        {
            var dto = new CharacterDto();

            dto.LoadFromModel(model);
            
            return dto;
        }
    }
}