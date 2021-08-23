using StoryBooks.Models;

namespace StoryBooks.Api.Dto.Actor
{
    public class CharacterUpdateDto : AbstractActorUpdateDto
    {
        public string Firstname { get; set; } = "";

        public string Lastname { get; set; } = "";

        public override string Name => $"{Firstname} {Lastname}";

        public void Patch(Character model)
        {
            base.Patch(model);
            model.Firstname = Firstname;
            model.Lastname = Lastname;
        }
    }
}