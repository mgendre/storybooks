namespace StoryBooks.Models
{
    public class Character : AbstractActor
    {
        public string Firstname { get; set; } = string.Empty;

        public string Lastname { get; set; } = string.Empty;
        
        public override string Name => $"{Firstname} {Lastname}";
    }
}
