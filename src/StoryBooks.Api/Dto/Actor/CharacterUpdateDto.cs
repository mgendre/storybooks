using StoryBooks.Models;

namespace StoryBooks.Api.Dto.Actor
{
    public class CharacterUpdateDto : AbstractActorUpdateDto
    {
        public string Firstname { get; set; } = string.Empty;

        public string Lastname { get; set; } = string.Empty;

        public override string Name => $"{Firstname} {Lastname}";

        public void Patch(Character model)
        {
            base.Patch(model);
            model.Firstname = Firstname;
            model.Lastname = Lastname;
        }
    }
}