using System;

namespace StoryBooks.Models
{
    public class Scenario
    {
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Markdown { get; set; }
    }
}