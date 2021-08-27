using System;
using StoryBooks.Models;

namespace StoryBooks.Api.Dto.Actor
{
    public abstract class AbstractActorDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string CampaignId { get; set; } = string.Empty;

        public virtual string Name { get; set; } = string.Empty;

        public string DescriptionMarkdown { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        public string Type { get; set; } = string.Empty;
        
        
        protected void LoadFromModel(AbstractActor model)
        {
            Name = model.Name;
            DescriptionMarkdown = model.DescriptionMarkdown;
            CreationDate = model.CreationDate;
            ModificationDate = model.ModificationDate;
            CampaignId = model.CampaignId;
            Id = model.Id;
            Type = model.Type;
        }
    }
}
