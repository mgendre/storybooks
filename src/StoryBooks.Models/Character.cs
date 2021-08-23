namespace StoryBooks.Models
{
    public class Character : AbstractActor
    {
        public string Firstname { get; set; }
        
        public string Lastname { get; set; }
        
        public override string Name => $"{Firstname} {Lastname}";
    }
}
