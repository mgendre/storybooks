using System;

namespace StoryBooks.Api.Dto
{
    public class ScenarioDto
    {
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Markdown { get; set; }
    }
}