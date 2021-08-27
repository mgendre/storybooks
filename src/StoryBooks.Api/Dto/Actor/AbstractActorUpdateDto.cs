using StoryBooks.Models;

namespace StoryBooks.Api.Dto.Actor
{
    public class AbstractActorUpdateDto
    {
        public virtual string Name { get; set; } = string.Empty;

        public string DescriptionMarkdown { get; set; } = string.Empty;

        public void Patch(AbstractActor actor)
        {
            actor.Name = Name;
            actor.DescriptionMarkdown = DescriptionMarkdown;
        }
    }
}
