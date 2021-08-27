using StoryBooks.Models;

namespace StoryBooks.Api.Dto
{
    public class CampaignUpdateDto
    {
        public string Name { get; set; } = string.Empty;

        public void Patch(Campaign toPatch)
        {
            toPatch.Name = Name;
        }
    }
}