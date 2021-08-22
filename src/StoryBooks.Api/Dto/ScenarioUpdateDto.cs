using System.Linq;
using StoryBooks.Models;

namespace StoryBooks.Api.Dto
{
    public class ScenarioUpdateDto
    {
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
    }
}