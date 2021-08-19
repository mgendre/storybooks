using System;
using StoryBooks.Models;

namespace StoryBooks.Api.Dto
{
    public class ScenarioDto
    {
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Markdown { get; set; }

        public static ScenarioDto FromModel(Scenario scenario)
        {
            return new ScenarioDto
            {
                Id = scenario.Id,
                Markdown = scenario.Markdown,
                Title = scenario.Title,
                CreationDate = scenario.CreationDate
            };
        }
    }
}