using StoryBooks.Models;

namespace StoryBooks.Api.Dto.Actor
{
    public class CharacterDto : AbstractActorDto
    {
        public string Firstname { get; set; } = string.Empty;

        public string Lastname { get; set; } = string.Empty;
        
        public override string Name => $"{Firstname} {Lastname}";

        public static CharacterDto FromModel(Character model)
        {
            var dto = new CharacterDto
            {
                Firstname = model.Firstname,
                Lastname = model.Lastname,
            };

            dto.LoadFromModel(model);
            
            return dto;
        }
    }
}