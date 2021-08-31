using StoryBooks.Models;

namespace StoryBooks.Api.Dto.Actor
{
    public class CharacterUpdateDto : AbstractActorUpdateDto
    {
        public void Patch(Character model)
        {
            base.Patch(model);
        }
    }
}