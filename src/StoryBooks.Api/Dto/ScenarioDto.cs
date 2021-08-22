using System;
using System.Linq;
using StoryBooks.Models;

namespace StoryBooks.Api.Dto
{
    public class ScenarioDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreationDate { get; set; }
        public string Title { get; set; } = "";
        public string Markdown { get; set; } = "";

        public void Patch(Scenario scenario)
        {
            scenario.Title = Title;
            if (!scenario.Sections.Any())
            {
                scenario.Sections = new[] { new Section() };
            }

            scenario.Sections.First().Markdown = Markdown;
        }

        public static ScenarioDto FromModel(Scenario scenario)
        {
            return new ScenarioDto
            {
                Id = scenario.Id,
                Markdown = scenario.Sections.FirstOrDefault()?.Markdown ?? "",
                Title = scenario.Title,
                CreationDate = scenario.CreationDate
            };
        }
    }
}